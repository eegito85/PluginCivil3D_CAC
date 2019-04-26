Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.Civil.ApplicationServices


Public Class Class1

    <CommandMethod("editaPolilinha3d")> _
    Public Sub CmdeditaPolilinha3d()
        ' acessar o Editor (linha de comando)
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor()

        ' acessar o CivilDocument, que gerencia os dados do Civil 3D
        Dim civilDoc As CivilDocument = CivilApplication.ActiveDocument
        Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument

        funcoes.selecionaPolilinha()

    End Sub

End Class
