<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormPlanilhaDados
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormPlanilhaDados))
        Me.dgvDadosDesenho = New System.Windows.Forms.DataGridView()
        Me.btOK = New System.Windows.Forms.Button()
        Me.nomeValeta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.x1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.y1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.z1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.x2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.y2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.z2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.extensao = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.declividade = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvDadosDesenho, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvDadosDesenho
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.dgvDadosDesenho.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDadosDesenho.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvDadosDesenho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDadosDesenho.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.nomeValeta, Me.x1, Me.y1, Me.z1, Me.x2, Me.y2, Me.z2, Me.extensao, Me.declividade})
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDadosDesenho.DefaultCellStyle = DataGridViewCellStyle12
        Me.dgvDadosDesenho.Location = New System.Drawing.Point(12, 12)
        Me.dgvDadosDesenho.Name = "dgvDadosDesenho"
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDadosDesenho.RowHeadersDefaultCellStyle = DataGridViewCellStyle13
        Me.dgvDadosDesenho.Size = New System.Drawing.Size(759, 480)
        Me.dgvDadosDesenho.TabIndex = 0
        '
        'btOK
        '
        Me.btOK.Location = New System.Drawing.Point(360, 507)
        Me.btOK.Name = "btOK"
        Me.btOK.Size = New System.Drawing.Size(75, 23)
        Me.btOK.TabIndex = 3
        Me.btOK.Text = "OK"
        Me.btOK.UseVisualStyleBackColor = True
        '
        'nomeValeta
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.PaleGoldenrod
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        Me.nomeValeta.DefaultCellStyle = DataGridViewCellStyle3
        Me.nomeValeta.HeaderText = "Nome da Valeta"
        Me.nomeValeta.Name = "nomeValeta"
        Me.nomeValeta.ReadOnly = True
        '
        'x1
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.AntiqueWhite
        Me.x1.DefaultCellStyle = DataGridViewCellStyle4
        Me.x1.HeaderText = "X1 (m)"
        Me.x1.Name = "x1"
        Me.x1.ReadOnly = True
        Me.x1.Width = 70
        '
        'y1
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.NavajoWhite
        Me.y1.DefaultCellStyle = DataGridViewCellStyle5
        Me.y1.HeaderText = "Y1 (m)"
        Me.y1.Name = "y1"
        Me.y1.ReadOnly = True
        Me.y1.Width = 70
        '
        'z1
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.z1.DefaultCellStyle = DataGridViewCellStyle6
        Me.z1.HeaderText = "Z1 (m)"
        Me.z1.Name = "z1"
        Me.z1.ReadOnly = True
        Me.z1.Width = 70
        '
        'x2
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.Moccasin
        Me.x2.DefaultCellStyle = DataGridViewCellStyle7
        Me.x2.HeaderText = "X2 (m)"
        Me.x2.Name = "x2"
        Me.x2.ReadOnly = True
        Me.x2.Width = 70
        '
        'y2
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.PapayaWhip
        Me.y2.DefaultCellStyle = DataGridViewCellStyle8
        Me.y2.HeaderText = "Y2 (m)"
        Me.y2.Name = "y2"
        Me.y2.ReadOnly = True
        Me.y2.Width = 70
        '
        'z2
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.Wheat
        Me.z2.DefaultCellStyle = DataGridViewCellStyle9
        Me.z2.HeaderText = "Z2 (m)"
        Me.z2.Name = "z2"
        Me.z2.ReadOnly = True
        Me.z2.Width = 70
        '
        'extensao
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.LemonChiffon
        DataGridViewCellStyle10.Format = "N2"
        DataGridViewCellStyle10.NullValue = Nothing
        Me.extensao.DefaultCellStyle = DataGridViewCellStyle10
        Me.extensao.HeaderText = "Extensão(m)"
        Me.extensao.Name = "extensao"
        Me.extensao.ReadOnly = True
        Me.extensao.Width = 75
        '
        'declividade
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.Khaki
        DataGridViewCellStyle11.NullValue = Nothing
        Me.declividade.DefaultCellStyle = DataGridViewCellStyle11
        Me.declividade.HeaderText = "Declividade(%)"
        Me.declividade.Name = "declividade"
        Me.declividade.ReadOnly = True
        '
        'FormPlanilhaDados
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(783, 542)
        Me.Controls.Add(Me.btOK)
        Me.Controls.Add(Me.dgvDadosDesenho)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(799, 580)
        Me.Name = "FormPlanilhaDados"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Dados da valeta"
        CType(Me.dgvDadosDesenho, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvDadosDesenho As System.Windows.Forms.DataGridView
    Friend WithEvents btOK As System.Windows.Forms.Button
    Friend WithEvents nomeValeta As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents x1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents y1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents z1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents x2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents y2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents z2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents extensao As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents declividade As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
