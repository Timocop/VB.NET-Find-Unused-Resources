<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If (disposing) Then
                CleanUp()
            End If

            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Button_Scan = New System.Windows.Forms.Button()
        Me.Button_FindProject = New System.Windows.Forms.Button()
        Me.TextBox_ProjectPath = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ListBox_Files = New System.Windows.Forms.ListBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ListBox_UnusedRes = New System.Windows.Forms.ListBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ListBox_UnusedFiles = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'Button_Scan
        '
        Me.Button_Scan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Scan.Location = New System.Drawing.Point(623, 393)
        Me.Button_Scan.Name = "Button_Scan"
        Me.Button_Scan.Size = New System.Drawing.Size(75, 23)
        Me.Button_Scan.TabIndex = 0
        Me.Button_Scan.Text = "Scan"
        Me.Button_Scan.UseVisualStyleBackColor = True
        '
        'Button_FindProject
        '
        Me.Button_FindProject.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_FindProject.Location = New System.Drawing.Point(666, 12)
        Me.Button_FindProject.Name = "Button_FindProject"
        Me.Button_FindProject.Size = New System.Drawing.Size(32, 23)
        Me.Button_FindProject.TabIndex = 1
        Me.Button_FindProject.Text = "..."
        Me.Button_FindProject.UseVisualStyleBackColor = True
        '
        'TextBox_ProjectPath
        '
        Me.TextBox_ProjectPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_ProjectPath.Location = New System.Drawing.Point(12, 14)
        Me.TextBox_ProjectPath.Name = "TextBox_ProjectPath"
        Me.TextBox_ProjectPath.Size = New System.Drawing.Size(648, 20)
        Me.TextBox_ProjectPath.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Files to scan:"
        '
        'ListBox_Files
        '
        Me.ListBox_Files.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox_Files.FormattingEnabled = True
        Me.ListBox_Files.Location = New System.Drawing.Point(12, 53)
        Me.ListBox_Files.Name = "ListBox_Files"
        Me.ListBox_Files.Size = New System.Drawing.Size(686, 95)
        Me.ListBox_Files.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 151)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Unused resources:"
        '
        'ListBox_UnusedRes
        '
        Me.ListBox_UnusedRes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox_UnusedRes.FormattingEnabled = True
        Me.ListBox_UnusedRes.Location = New System.Drawing.Point(12, 167)
        Me.ListBox_UnusedRes.Name = "ListBox_UnusedRes"
        Me.ListBox_UnusedRes.Size = New System.Drawing.Size(686, 95)
        Me.ListBox_UnusedRes.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 265)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(112, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Unused resource files:"
        '
        'ListBox_UnusedFiles
        '
        Me.ListBox_UnusedFiles.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox_UnusedFiles.FormattingEnabled = True
        Me.ListBox_UnusedFiles.Location = New System.Drawing.Point(12, 281)
        Me.ListBox_UnusedFiles.Name = "ListBox_UnusedFiles"
        Me.ListBox_UnusedFiles.Size = New System.Drawing.Size(686, 95)
        Me.ListBox_UnusedFiles.TabIndex = 8
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(710, 428)
        Me.Controls.Add(Me.ListBox_UnusedFiles)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ListBox_UnusedRes)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ListBox_Files)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox_ProjectPath)
        Me.Controls.Add(Me.Button_FindProject)
        Me.Controls.Add(Me.Button_Scan)
        Me.Name = "FormMain"
        Me.Text = "VB.NET Find Unused Resources"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button_Scan As Button
    Friend WithEvents Button_FindProject As Button
    Friend WithEvents TextBox_ProjectPath As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents ListBox_Files As ListBox
    Friend WithEvents Label2 As Label
    Friend WithEvents ListBox_UnusedRes As ListBox
    Friend WithEvents Label3 As Label
    Friend WithEvents ListBox_UnusedFiles As ListBox
End Class
