Imports System.Resources
Imports System.Text.RegularExpressions

Public Class FormMain
    Private g_ClassScanner As ClassScanner

    Private Sub Button_Scan_Click(sender As Object, e As EventArgs) Handles Button_Scan.Click
        Try
            If (g_ClassScanner Is Nothing OrElse Not g_ClassScanner.m_Scanning) Then
                g_ClassScanner = New ClassScanner(Me, TextBox_ProjectPath.Text)
                g_ClassScanner.Start()
            Else
                MessageBox.Show("A scan is already running!")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button_FindProject_Click(sender As Object, e As EventArgs) Handles Button_FindProject.Click
        Try
            Using i As New OpenFileDialog()
                i.Filter = "Visual Basic Project|*.vbproj"

                If (i.ShowDialog = DialogResult.OK) Then
                    TextBox_ProjectPath.Text = i.FileName
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CleanUp()
        If (g_ClassScanner IsNot Nothing) Then
            g_ClassScanner.Abort()
            g_ClassScanner = Nothing
        End If
    End Sub

    Class ClassScanner
        Private g_fFormMain As FormMain

        Private g_mScannerThread As Threading.Thread
        Private g_sProjectPath As String = ""

        Public Sub New(f As FormMain, sProjectPath As String)
            g_fFormMain = f
            g_sProjectPath = sProjectPath

            If (Not IO.File.Exists(sProjectPath)) Then
                Throw New ArgumentException("Project file does not exist")
            End If
        End Sub

        Public Sub Start()
            If (m_Scanning) Then
                Return
            End If

            g_mScannerThread = New Threading.Thread(AddressOf ScannerThread) With {
                .IsBackground = True
            }
            g_mScannerThread.Start()
        End Sub

        Public Sub Abort()
            If (m_Scanning) Then
                g_mScannerThread.Abort()
                g_mScannerThread.Join()
                g_mScannerThread = Nothing
            End If
        End Sub

        ReadOnly Property m_Scanning As Boolean
            Get
                Return (g_mScannerThread IsNot Nothing AndAlso g_mScannerThread.IsAlive)
            End Get
        End Property

        Private Sub ScannerThread()
            Try
                Dim sRootFolder As String = IO.Path.GetDirectoryName(g_sProjectPath)
                Dim sResourcePath As String = IO.Path.Combine(sRootFolder, "Resources")

                If (Not IO.Directory.Exists(sResourcePath)) Then
                    Throw New ArgumentException("Resource directory does not exist")
                End If

                'Get all files from the project
                Dim lProjectFiles As New List(Of String)
                Using mProjReader As New Xml.XmlTextReader(g_sProjectPath)
                    While (mProjReader.Read())
                        Select Case mProjReader.NodeType
                            Case (Xml.XmlNodeType.Element)
                                If (mProjReader.Name.ToLower <> "compile" OrElse Not mProjReader.HasAttributes) Then
                                    Continue While
                                End If

                                While mProjReader.MoveToNextAttribute()
                                    If (mProjReader.Name.ToLower <> "include") Then
                                        Continue While
                                    End If

                                    lProjectFiles.Add(IO.Path.Combine(sRootFolder, mProjReader.Value))
                                End While
                        End Select
                    End While
                End Using

                'Read all files
                Dim lFilesContent As New List(Of DictionaryEntry)
                For Each sFile As String In lProjectFiles
                    If (Not IO.File.Exists(sFile)) Then
                        Continue For
                    End If

                    lFilesContent.Add(New DictionaryEntry(sFile, IO.File.ReadAllText(sFile)))
                Next

                'Get all resources
                Dim mResList As New List(Of DictionaryEntry)
                Using mResReader As New Resources.ResXResourceReader(IO.Path.Combine(sRootFolder, "My Project\Resources.resx"))
                    mResReader.UseResXDataNodes = True
                    mResReader.BasePath = sResourcePath

                    For Each resourceValue As DictionaryEntry In mResReader
                        If (TypeOf resourceValue.Key IsNot String) Then
                            Continue For
                        End If

                        If (TypeOf resourceValue.Value IsNot ResXDataNode) Then
                            Continue For
                        End If

                        Dim mResNode As ResXDataNode = DirectCast(resourceValue.Value, ResXDataNode)
                        Dim sResName As String = CStr(resourceValue.Key)
                        Dim sFile As String = IO.Path.GetFullPath(mResNode.FileRef.FileName) 'Relative path to absolute path convertation

                        mResList.Add(New DictionaryEntry(sResName, sFile))
                    Next
                End Using

                'Create regex tests
                Dim mRegexTests As New List(Of DictionaryEntry)
                For Each mResDic In mResList
                    Dim sResName As String = CStr(mResDic.Key)

                    mRegexTests.Add(New DictionaryEntry(sResName, New Regex(String.Format("(My\.(Resources\.){0}{1})", "{1,2}", Regex.Escape(sResName)), RegexOptions.Multiline Or RegexOptions.Compiled)))
                    mRegexTests.Add(New DictionaryEntry(sResName, New Regex(String.Format("\(""{0}""\)", Regex.Escape(sResName)), RegexOptions.Multiline Or RegexOptions.Compiled)))
                Next

                'Check in files
                Dim lUnusedRes As New List(Of String)
                Dim lUsedRes As New List(Of String)
                For Each mRegexDic In mRegexTests
                    Dim sResName As String = CStr(mRegexDic.Key)
                    Dim mRegex As Regex = DirectCast(mRegexDic.Value, Regex)

                    If (lUnusedRes.Contains(sResName) OrElse
                                lUsedRes.Contains(sResName)) Then
                        Continue For
                    End If

                    For Each mFileDic In lFilesContent
                        Dim sFile As String = CStr(mFileDic.Key)
                        Dim sContent As String = CStr(mFileDic.Value)

                        If (mRegex.IsMatch(sContent)) Then
                            lUsedRes.Add(sResName)
                            Exit For
                        End If
                    Next
                Next

                'Get unused resource names
                For Each mResDic In mResList
                    Dim sResName As String = CStr(mResDic.Key)

                    If (lUsedRes.Contains(sResName)) Then
                        Continue For
                    End If

                    lUnusedRes.Add(sResName)
                Next

                'Get unused files
                Dim lUnusedFiles As New List(Of String)
                For Each sRegFile As String In IO.Directory.GetFiles(sResourcePath)
                    Dim bUnused As Boolean = True

                    For Each mResDic In mResList
                        Dim sResFicFile As String = CStr(mResDic.Value)

                        If (sResFicFile.ToLower <> sRegFile.ToLower) Then
                            Continue For
                        End If

                        bUnused = False
                        Exit For
                    Next

                    If (bUnused) Then
                        lUnusedFiles.Add(sRegFile)
                    End If
                Next


                g_fFormMain.BeginInvoke(Sub()
                                            g_fFormMain.ListBox_Files.Items.Clear()
                                            g_fFormMain.ListBox_UnusedRes.Items.Clear()
                                            g_fFormMain.ListBox_UnusedFiles.Items.Clear()

                                            'Files scanned
                                            g_fFormMain.ListBox_Files.BeginUpdate()
                                            For Each mFileDic In lFilesContent
                                                g_fFormMain.ListBox_Files.Items.Add(CStr(mFileDic.Key))
                                            Next
                                            g_fFormMain.ListBox_Files.EndUpdate()

                                            'Unused res
                                            g_fFormMain.ListBox_UnusedRes.BeginUpdate()
                                            For Each sResName In lUnusedRes
                                                g_fFormMain.ListBox_UnusedRes.Items.Add(sResName)
                                            Next
                                            g_fFormMain.ListBox_UnusedRes.EndUpdate()

                                            'Unused files
                                            g_fFormMain.ListBox_UnusedFiles.BeginUpdate()
                                            For Each sResFile In lUnusedFiles
                                                g_fFormMain.ListBox_UnusedFiles.Items.Add(sResFile)
                                            Next
                                            g_fFormMain.ListBox_UnusedFiles.EndUpdate()
                                        End Sub)

                MessageBox.Show("Finished!", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Threading.ThreadAbortException
                Throw
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
    End Class
End Class
