Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.Civil.ApplicationServices
Imports Autodesk.AutoCAD.Geometry

Public Class Class1

    <CommandMethod("insereInfosPol2d")> _
    Public Sub CmdinsereInfosPol2d()
        ' acessar o Editor (linha de comando)
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor()
        ' acessar o CivilDocument, que gerencia os dados do Civil 3D
        Dim civilDoc As CivilDocument = CivilApplication.ActiveDocument
        Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument
        Dim meuFormulario As System.Windows.Forms.Form
        meuFormulario = New FormPlanilhaDados
        Dim dados As String = ""
        dados = funcoes.selecionaPolilinha2d()
        'MsgBox(dados)
        meuFormulario.Text = dados
        meuFormulario.ShowDialog()
    End Sub

End Class
