Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.EditorInput
Imports System.IO
Imports Autodesk.AutoCAD.Colors

Module funcoes

    Const pi As Double = 3.141592654
    Dim extFinal As Double = 0

    Public Function selecionaPolilinha2d() As String

        Dim doc As Document = Application.DocumentManager.MdiActiveDocument
        Dim ed As Editor = doc.Editor
        Dim db = doc.Database
        Dim objeto As ObjectId = ObjectId.Null
        Dim peo As PromptEntityOptions = New PromptEntityOptions(vbLf + "selecione uma polilinha 2d:")
        Dim per As PromptEntityResult = ed.GetEntity(peo)
        Dim tipo As String
        Dim numSegmentos As Integer = 0
        Dim strFinal As String = ""
        Dim nmVlt As String = ""

        Dim pStrOpts As PromptStringOptions = New PromptStringOptions(vbLf & "Digite o nome da valeta: ")
        pStrOpts.AllowSpaces = True
        Dim pStrRes As PromptResult = doc.Editor.GetString(pStrOpts)
        Dim nomeValeta = pStrRes.StringResult

        If (per.Status = PromptStatus.OK) Then
            Dim tr As Transaction = db.TransactionManager.StartTransaction()
            Using (tr)
                objeto = per.ObjectId
                Dim obj As DBObject = tr.GetObject(objeto, OpenMode.ForRead)
                Dim ent As Entity = CType(obj, Entity)
                tipo = ent.GetType.ToString
                'ed.WriteMessage(vbLf + tipo)
                Dim tamanhoTotal As Integer = 0
                'PEGANDO OS VÉRTICES DA POLILINHAS 2D
                If (tipo = "Autodesk.AutoCAD.DatabaseServices.Polyline") Then
                    Dim lwp As Polyline = obj
                    Dim vn As Integer = lwp.NumberOfVertices
                    tamanhoTotal = vn
                    Dim pt As Point2d
                    Dim x(tamanhoTotal) As Double
                    Dim y(tamanhoTotal) As Double
                    Dim extensaoTotal As String = ""
                    For i As Integer = 0 To (vn - 1)
                        pt = lwp.GetPoint2dAt(i)
                        x(i) = Convert.ToDouble(pt.X)
                        y(i) = Convert.ToDouble(pt.Y)
                    Next
                    numSegmentos = tamanhoTotal - 1
                    '------>>>>> PEGANDO A REFERÊNCIA EXTERNA DA NOTA
                    Dim objEXTDT As DBObject = tr.GetObject(lwp.ObjectId, Autodesk.AutoCAD.DatabaseServices.OpenMode.ForRead)
                    Dim extDict As DBDictionary = tr.GetObject(objEXTDT.ExtensionDictionary(), Autodesk.AutoCAD.DatabaseServices.OpenMode.ForRead)
                    Dim strNotes As String = ""
                    strNotes = Autodesk.Aec.DatabaseServices.TextNote.GetStandardNote(objEXTDT)
                    '------>>>>> CONSTRUINDO A STRING PRA EXPORTAR PRA TABELA
                    Dim strDados As String = strNotes
                    strFinal = funcoes.constroiStrTabela(strDados, nomeValeta)
                    extensaoTotal = Convert.ToString(Math.Round(extFinal, 2))
                    nmVlt = nomeValeta & "\P0,5x0,5;1/1;L=" & extensaoTotal & "m"
                    funcoes.insereNomeValeta(x, y, tamanhoTotal, nmVlt)
                End If
                tr.Commit()
            End Using
        End If
        'MsgBox(strFinal)
        Return strFinal
    End Function

    Public Sub insereTexto(ByVal pontoInsercao As Point3d, ByVal texto As String, ByVal altura As Double, ByVal angulo As Double)

        Dim Doc As Document = Application.DocumentManager.MdiActiveDocument
        Dim Db As Database = Doc.Database

        Using Trans As Transaction = Db.TransactionManager.StartTransaction()

            Dim BlkTbl As BlockTable = Trans.GetObject(Db.BlockTableId, OpenMode.ForRead)
            Dim BlkTblRec As BlockTableRecord = Trans.GetObject(BlkTbl(BlockTableRecord.ModelSpace), OpenMode.ForWrite)

            '' CRIANDO A LAYER PARA O TEXTO
            Dim acLyrTbl As LayerTable
            acLyrTbl = Trans.GetObject(Db.LayerTableId, OpenMode.ForRead)

            Dim sLayerName As String = "layerNomeValeta"
            If acLyrTbl.Has(sLayerName) = False Then
                Dim acLyrTblRec As LayerTableRecord = New LayerTableRecord()

                '' Assign the layer the ACI color 1 and a name
                acLyrTblRec.Color = Color.FromColorIndex(ColorMethod.ByLayer, 1)
                acLyrTblRec.Name = sLayerName

                '' Upgrade the Layer table for write
                acLyrTbl.UpgradeOpen()

                '' Append the new layer to the Layer table and the transaction
                acLyrTbl.Add(acLyrTblRec)
                Trans.AddNewlyCreatedDBObject(acLyrTblRec, True)
            End If

            Db.Clayer = acLyrTbl(sLayerName)

            '' CRIANDO O TEXTO
            Dim cor As Color = Color.FromColorIndex(ColorMethod.ByLayer, 1)
            Dim acMText As MText = New MText
            acMText.SetDatabaseDefaults()
            acMText.Location = pontoInsercao
            acMText.TextHeight = 1.5
            acMText.Contents = texto
            acMText.Rotation = angulo
            acMText.Layer = sLayerName
            acMText.Color = cor
            acMText.LineSpaceDistance = 1.5
            acMText.Attachment = AttachmentPoint.TopCenter

            BlkTblRec.AppendEntity(acMText)
            Trans.AddNewlyCreatedDBObject(acMText, True)

            Trans.Commit()

        End Using

    End Sub

    Public Sub insereNomeValeta(ByVal x() As Double, ByVal y() As Double, ByVal tamanho As Integer, ByVal nomeValeta As String)

        Dim ptInsercaoValeta As Point3d = New Point3d(0, 0, 0)
        Dim indice As Integer = tamanho \ 2
        Dim xValeta As Double = ((x(indice - 1) + x(indice)) / 2)
        Dim yValeta As Double = ((y(indice - 1) + y(indice)) / 2)
        Dim distancia As Double = Math.Sqrt(((x(indice - 1) - x(indice)) * (x(indice - 1) - x(indice))) + ((y(indice - 1) - y(indice)) * (y(indice - 1) - y(indice))))
        Dim alturaTexto As Double = 1.5
        Dim deslocVerticalNomeValeta As Double = 0
        If (distancia >= 106) Then
            'alturaTexto = 30
            deslocVerticalNomeValeta = 30
        Else
            'alturaTexto = (30 * distancia) / 106
            deslocVerticalNomeValeta = -0.85
        End If
        Dim angulo As Double = 0
        angulo = funcoes.pegaAngulo(x(indice - 1), y(indice - 1), x(indice), y(indice))
        yValeta = yValeta + deslocVerticalNomeValeta
        ptInsercaoValeta = New Point3d(xValeta, yValeta, 0)

        funcoes.insereTexto(ptInsercaoValeta, nomeValeta, alturaTexto, angulo)

    End Sub

    Public Function pegaAngulo(ByVal xi As Double, ByVal yi As Double, ByVal xf As Double, ByVal yf As Double) As Double

        Dim angulo As Double = 0

        If (xf <> xi) Then
            angulo = Math.Atan((yf - yi) / (xf - xi))
        Else
            angulo = pi / 2
        End If

        Return angulo

    End Function

    Public Function SelecionarPonto() As Point3d
        'Acessar o editor do AutoCAD 
        '(linha de comando)
        Dim ed As Editor = Application.DocumentManager. _
                            MdiActiveDocument.Editor
        'Opções de seleção de um ponto
        Dim opcoesPonto As New PromptPointOptions(vbLf + "Selecione o ponto para inserção da tabela")
        'Para para o editor - que irá pedir ao
        'usuário - e guardar o resultado
        Dim resultadoPonto As PromptPointResult = ed.GetPoint(opcoesPonto)
        'Acessar o ponto clicado
        Dim pontoClicado As Point3d = resultadoPonto.Value

        Return pontoClicado

    End Function

    Public Function constroiStrTabela(ByVal strPontos As String, ByVal valeta As String) As String

        Dim strFinal As String = ""
        Dim matrizPts0() As String = strPontos.Split("#")
        Dim tamanho0 As Integer = matrizPts0.Length
        Dim tamanho As Integer = 0
        For i As Integer = 0 To (tamanho0 - 1)
            If (matrizPts0(i) <> "") Then
                tamanho = tamanho + 1
            End If
        Next
        Dim matrizPts(tamanho) As String
        Dim j As Integer = 0
        For i As Integer = 0 To (tamanho0 - 1)
            If (matrizPts0(i) <> "") Then
                matrizPts(j) = matrizPts0(i)
                j = j + 1
            End If
        Next
        'For i As Integer = 0 To (tamanho - 1)
        '    MsgBox(matrizPts(i))
        'Next
        Dim x(tamanho) As Double
        Dim y(tamanho) As Double
        Dim z(tamanho) As Double
        Dim extensao(tamanho - 1) As Double
        Dim declividade(tamanho - 1) As Double
        Dim strQ(3) As String

        For i As Integer = 0 To (tamanho - 1)
            strQ = matrizPts(i).Split("*")
            x(i) = Math.Round(Convert.ToDouble(strQ(0)), 8)
            y(i) = Math.Round(Convert.ToDouble(strQ(1)), 8)
            z(i) = Math.Round(Convert.ToDouble(strQ(2)), 8)
        Next
        Dim dx As Double = 0
        Dim dy As Double = 0
        Dim dz As Double = 0


        For i As Integer = 0 To (tamanho - 2)
            dx = (x(i + 1) - x(i)) * (x(i + 1) - x(i))
            dy = (y(i + 1) - y(i)) * (y(i + 1) - y(i))
            dz = (z(i + 1) - z(i)) * (z(i + 1) - z(i))
            extensao(i) = Math.Round(Math.Sqrt(dx + dy + dz), 2)
            extFinal = extFinal + extensao(i)
            declividade(i) = Math.Round(((100 * (z(i + 1) - z(i))) / (Math.Sqrt(dx + dy))), 4)
            strFinal = strFinal & "#" & (Convert.ToString(x(i))) & "*" & (Convert.ToString(y(i))) & "*" & (Convert.ToString(z(i))) & "*" & (Convert.ToString(x(i + 1))) & "*" & (Convert.ToString(y(i + 1))) & "*" & (Convert.ToString(z(i + 1))) & "*" & Convert.ToString(extensao(i)) & "*" & Convert.ToString(declividade(i))
        Next

        strFinal = valeta & strFinal

        Return strFinal

    End Function

End Module
