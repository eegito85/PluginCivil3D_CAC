Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.Civil.ApplicationServices
Imports Autodesk.AutoCAD
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.Colors
Imports System.IO

Module funcoes

    Const pi As Double = 3.141592654

    Public Sub selecionaPolilinha()

        'ESSA SUBROTINA É RESPONSÁVEL PELA SELEÇÃO DA POLILINHA 3D E DA LEITURA DOS PONTOS QUE COMPÕEM SEUS VÉRTICES

        Dim doc As Document = Application.DocumentManager.MdiActiveDocument
        Dim ed As Editor = doc.Editor
        Dim db = doc.Database
        Dim objeto As ObjectId = ObjectId.Null
        Dim peo As PromptEntityOptions = New PromptEntityOptions(vbLf + "selecione uma polilinha 3d:")
        Dim per As PromptEntityResult = ed.GetEntity(peo)
        Dim tipo As String

        If (per.Status = PromptStatus.OK) Then
            Dim tr As Transaction = db.TransactionManager.StartTransaction()
            Using (tr)
                objeto = per.ObjectId
                Dim obj As DBObject = tr.GetObject(objeto, OpenMode.ForWrite)
                Dim ent As Entity = CType(obj, Entity)
                tipo = ent.GetType.ToString
                'ed.WriteMessage(vbLf + tipo)
                Dim tamanhoTotal As Integer = 0

                If (tipo = "Autodesk.AutoCAD.DatabaseServices.Polyline3d") Then
                    Dim p3d As Polyline3d = obj
                    Dim j As Integer = 0
                    For Each vId As ObjectId In p3d
                        j = j + 1
                    Next
                    tamanhoTotal = j
                    Dim k As Integer = 0
                    Dim x(tamanhoTotal) As Double
                    Dim y(tamanhoTotal) As Double
                    Dim z(tamanhoTotal) As Double
                    For Each vId As ObjectId In p3d
                        Dim v3d As PolylineVertex3d = tr.GetObject(vId, OpenMode.ForRead)
                        x(k) = Convert.ToDouble(v3d.Position.X.ToString)
                        y(k) = Convert.ToDouble(v3d.Position.Y.ToString)
                        z(k) = Convert.ToDouble(v3d.Position.Z.ToString)
                        k = k + 1
                    Next


                    funcoes.geraPolilinhas2D(x, y, z, tamanhoTotal)
                    'funcoes.nomeiaPolilinha2d(x, y, z, tamanhoTotal)


                End If

                'obj.Erase()

                tr.Commit()
            End Using
        End If

        'ed.UpdateScreen()

        'ed.Regen()

    End Sub

    Public Sub construirPolilinha2d(ByVal x() As Double, ByVal y() As Double, ByVal z() As Double, ByVal tamanho As Integer)
        Dim Doc As Document = Application.DocumentManager.MdiActiveDocument
        Dim Db As Database = Doc.Database
        Dim notaReferencia As String = "#"
        Using Trans As Transaction = Db.TransactionManager.StartTransaction()

            Dim BlkTbl As BlockTable = Trans.GetObject(Db.BlockTableId, OpenMode.ForRead)
            Dim BlkTblRec As BlockTableRecord = Trans.GetObject(BlkTbl(BlockTableRecord.ModelSpace), OpenMode.ForWrite)
            '---->>>CONFIGURANDO A COR
            Dim cor As Color
            cor = Color.FromColorIndex(ColorMethod.ByAci, 5)
            '--->>> CONFIGURANDO O TIPO DE LINHA
            Dim acLineTypTbl As LinetypeTable
            acLineTypTbl = Trans.GetObject(Db.LinetypeTableId, OpenMode.ForRead)
            Dim sLineTypName As String = ""

            If (z(0) < z(tamanho - 1)) Then
                sLineTypName = "002-Valeta D40_esquerda"
            End If
            If (z(0) > z(tamanho - 1)) Then
                sLineTypName = "003-Valeta D40_direita"
            End If

            '' CRIANDO A LAYER PARA A POLILINHA 2D
            Dim acLyrTbl As LayerTable
            acLyrTbl = Trans.GetObject(Db.LayerTableId, OpenMode.ForRead)

            Dim sLayerName As String = "layerPendenciaValeta"
            If acLyrTbl.Has(sLayerName) = False Then
                Dim acLyrTblRec As LayerTableRecord = New LayerTableRecord()
                acLyrTblRec.LineWeight = LineWeight.LineWeight035
                acLyrTblRec.Color = Color.FromColorIndex(ColorMethod.ByLayer, 5)
                acLyrTblRec.Name = sLayerName
                acLyrTbl.UpgradeOpen()

                acLyrTbl.Add(acLyrTblRec)
                Trans.AddNewlyCreatedDBObject(acLyrTblRec, True)
            End If

            'CONSTRUINDO A POLILINHA 2D
            Dim Poly As Polyline = New Polyline()
            Poly.SetDatabaseDefaults()
            For i As Integer = 0 To (tamanho - 1)
                Poly.AddVertexAt(i, New Point2d(x(i), y(i)), 0, 0, 0)
                Poly.Color = cor
                If acLineTypTbl.Has(sLineTypName) = False Then
                Else
                    Poly.Linetype = sLineTypName
                    Poly.LineWeight = LineWeight.LineWeight035
                End If
                notaReferencia = notaReferencia & Convert.ToString(x(i)) & "*" & Convert.ToString(y(i)) & "*" & Convert.ToString(z(i)) & "#"
            Next

            Poly.Layer = sLayerName
            Poly.LinetypeScale = 4
            Db.Clayer = acLyrTbl(sLayerName)
            Poly.Plinegen = True

            BlkTblRec.AppendEntity(Poly)
            Trans.AddNewlyCreatedDBObject(Poly, True)

            '------------>>>NOVA ALTERAÇÃO
            Dim objeto As DBObject = Trans.GetObject(Poly.ObjectId, Autodesk.AutoCAD.DatabaseServices.OpenMode.ForWrite)
            objeto.CreateExtensionDictionary()
            Dim txt As Autodesk.Aec.DatabaseServices.TextNote = New Autodesk.Aec.DatabaseServices.TextNote()
            txt.Note = notaReferencia
            Dim extDict As DBDictionary = Trans.GetObject(objeto.ExtensionDictionary(), Autodesk.AutoCAD.DatabaseServices.OpenMode.ForWrite, False)
            extDict.SetAt(Autodesk.Aec.DatabaseServices.TextNote.ExtensionDictionaryName, txt)

            Trans.AddNewlyCreatedDBObject(txt, True)

            Trans.Commit()
        End Using

    End Sub

    'Public Sub estaqueamento(ByVal x() As Double, ByVal y() As Double, ByVal z() As Double, ByVal tamanho As Integer)
    '
    '
    '
    '
    'End Sub


    Public Sub nomeiaPolilinha2d(ByVal x() As Double, ByVal y() As Double, ByVal z() As Double, ByVal tamanho As Integer)

        Dim distancias(tamanho - 1) As Double
        Dim declividades(tamanho - 1) As Double
        Dim quadradoDistancias(tamanho - 1) As Double
        Dim xMedio(tamanho - 1) As Double
        Dim yMedio(tamanho - 1) As Double
        Dim angulo(tamanho - 1) As Double
        Dim ptInsercao As Point3d = New Point3d(0, 0, 0)
        Dim alturaTexto As Double = 5
        Dim deslocXdistancia As Double = 0
        Dim deslocYdistancia As Double = 0
        Dim deslocXdeclividade As Double = 0
        Dim deslocYdeclividade As Double = 0
        Dim ddi As Double = 0
        Dim dde As Double = 0

        For i As Integer = 0 To (tamanho - 2)
            quadradoDistancias(i) = ((x(i + 1) - x(i)) * (x(i + 1) - x(i))) + ((y(i + 1) - y(i)) * (y(i + 1) - y(i))) + ((z(i + 1) - z(i)) * (z(i + 1) - z(i)))
            distancias(i) = Math.Sqrt(quadradoDistancias(i))
            declividades(i) = (100 * (z(i + 1) - z(i))) / (Math.Sqrt(((x(i + 1) - x(i)) * (x(i + 1) - x(i))) + ((y(i + 1) - y(i)) * (y(i + 1) - y(i)))))
            distancias(i) = Math.Round(distancias(i), 2)
            declividades(i) = Math.Round(declividades(i), 4)
            xMedio(i) = (x(i + 1) + x(i)) / 2
            yMedio(i) = (y(i + 1) + y(i)) / 2
            angulo(i) = funcoes.pegaAngulo(x(i), y(i), x(i + 1), y(i + 1))
            If (distancias(i) >= 106) Then
                alturaTexto = 5
                deslocXdistancia = 8 * (Math.Sin(angulo(i)))
                deslocYdistancia = 8 * (Math.Cos(angulo(i)))
                deslocXdeclividade = 20 * (Math.Sin(angulo(i)))
                deslocYdeclividade = 20 * (Math.Cos(angulo(i)))
            Else
                alturaTexto = (5 * distancias(i)) / 106
                ddi = (8 * alturaTexto) / 5
                dde = (4 * alturaTexto)
                deslocXdistancia = ddi * (Math.Sin(angulo(i)))
                deslocYdistancia = ddi * (Math.Cos(angulo(i)))
                deslocXdeclividade = dde * (Math.Sin(angulo(i)))
                deslocYdeclividade = dde * (Math.Cos(angulo(i)))
            End If

            ptInsercao = New Point3d(xMedio(i) + deslocXdistancia, yMedio(i) - deslocYdistancia, 0)
            funcoes.insereTexto(ptInsercao, "L(m): " & distancias(i).ToString, alturaTexto, angulo(i))
            ptInsercao = New Point3d(xMedio(i) + deslocXdeclividade, yMedio(i) - deslocYdeclividade, 0)
            funcoes.insereTexto(ptInsercao, "(%): " & declividades(i).ToString, alturaTexto, angulo(i))
        Next



    End Sub

    Public Sub insereTexto(ByVal pontoInsercao As Point3d, ByVal texto As String, ByVal altura As Double, ByVal angulo As Double)

        Dim Doc As Document = Application.DocumentManager.MdiActiveDocument
        Dim Db As Database = Doc.Database

        Using Trans As Transaction = Db.TransactionManager.StartTransaction()

            Dim BlkTbl As BlockTable = Trans.GetObject(Db.BlockTableId, OpenMode.ForRead)
            Dim BlkTblRec As BlockTableRecord = Trans.GetObject(BlkTbl(BlockTableRecord.ModelSpace), OpenMode.ForWrite)

            '' CRIANDO A LAYER PARA O TEXTO
            Dim acLyrTbl As LayerTable
            acLyrTbl = Trans.GetObject(Db.LayerTableId, OpenMode.ForRead)

            Dim sLayerName As String = "layerInformacoesValeta"
            If acLyrTbl.Has(sLayerName) = False Then
                Dim acLyrTblRec As LayerTableRecord = New LayerTableRecord()

                '' Assign the layer the ACI color 1 and a name
                acLyrTblRec.Color = Color.FromColorIndex(ColorMethod.ByAci, 3)
                acLyrTblRec.Name = sLayerName

                '' Upgrade the Layer table for write
                acLyrTbl.UpgradeOpen()

                '' Append the new layer to the Layer table and the transaction
                acLyrTbl.Add(acLyrTblRec)
                Trans.AddNewlyCreatedDBObject(acLyrTblRec, True)
            End If

            '' CRIANDO O TEXTO
            Dim acText As DBText = New DBText()
            acText.SetDatabaseDefaults()
            acText.Position = pontoInsercao
            acText.Height = altura
            acText.TextString = texto
            acText.Rotation = angulo
            acText.Layer = sLayerName

            BlkTblRec.AppendEntity(acText)
            Trans.AddNewlyCreatedDBObject(acText, True)

            Trans.Commit()

        End Using

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

    Public Sub constroiCirculo(ByVal ptInsercao As Point3d, ByVal raio As Double)

        Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument
        Dim acCurDb As Database = acDoc.Database


        Using acTrans As Transaction = acCurDb.TransactionManager.StartTransaction()

            Dim acBlkTbl As BlockTable
            acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead)
            Dim acBlkTblRec As BlockTableRecord
            acBlkTblRec = acTrans.GetObject(acBlkTbl(BlockTableRecord.ModelSpace), OpenMode.ForWrite)

            '' CRIANDO A LAYER PARA O TEXTO
            Dim acLyrTbl As LayerTable
            acLyrTbl = acTrans.GetObject(acCurDb.LayerTableId, OpenMode.ForRead)

            Dim sLayerName As String = "layerConfluenciaValetas"
            If acLyrTbl.Has(sLayerName) = False Then
                Dim acLyrTblRec As LayerTableRecord = New LayerTableRecord()

                '' Assign the layer the ACI color 1 and a name
                acLyrTblRec.Color = Color.FromColorIndex(ColorMethod.ByAci, 5)
                acLyrTblRec.LineWeight = LineWeight.LineWeight040
                acLyrTblRec.Name = sLayerName

                '' Upgrade the Layer table for write
                acLyrTbl.UpgradeOpen()

                '' Append the new layer to the Layer table and the transaction
                acLyrTbl.Add(acLyrTblRec)
                acTrans.AddNewlyCreatedDBObject(acLyrTblRec, True)
            End If

            '' Create a circle that is at 2,3 with a radius of 4.25
            Dim acCirc As Circle = New Circle()
            acCirc.SetDatabaseDefaults()
            acCirc.Center = ptInsercao
            acCirc.Radius = raio
            acCirc.Layer = sLayerName
            acCirc.Color = Color.FromColorIndex(ColorMethod.ByAci, 5)
            acCirc.LineWeight = LineWeight.LineWeight040

            acBlkTblRec.AppendEntity(acCirc)
            acTrans.AddNewlyCreatedDBObject(acCirc, True)
            acTrans.Commit()
        End Using
    End Sub

    Public Sub geraPolilinhas2D(ByVal x() As Double, ByVal y() As Double, ByVal z() As Double, ByVal tamanho As Integer)

        Dim ptInsercao As Point3d = New Point3d(0, 0, 0)
        'LISTA COM AS POSIÇÕES NA POLILINHA 3D ONDE OCORRE A MUDANÇA NO SENTIDO DE DECLIVIDADE
        Dim listaPosicoesZ As List(Of Integer) = New List(Of Integer)

        For i As Integer = 0 To (tamanho - 3)
            If ((z(i + 1) - z(i)) * (z(i + 2) - z(i + 1)) <= 0) Then
                listaPosicoesZ.Add((i + 1))
                If ((z(i + 1) <= z(i)) And (z(i + 1) <= z(i + 2))) Then
                    ptInsercao = New Point3d(x(i + 1), y(i + 1), 0)
                    'funcoes.constroiCirculo(ptInsercao, 0.75)
                End If
            End If
        Next
        Dim tamanhoLista As Integer = listaPosicoesZ.Count
        Dim numeroPolilinhas2d As Integer = tamanhoLista + 1
        If (numeroPolilinhas2d > 1) Then
            Dim tamanhoPolilinha2d(numeroPolilinhas2d) As Integer
            'DEFININDO O TAMANHO DE CADA POLILINHA 2D
            For i As Integer = 0 To (numeroPolilinhas2d - 1)
                If (i = 0) Then
                    tamanhoPolilinha2d(i) = listaPosicoesZ.Item(i) + 1
                End If
                If (i = (numeroPolilinhas2d - 1)) Then
                    tamanhoPolilinha2d(i) = tamanho - listaPosicoesZ.Item(i - 1)
                End If
                If ((i <> 0) And (i <> (numeroPolilinhas2d - 1))) Then
                    tamanhoPolilinha2d(i) = listaPosicoesZ.Item(i) - listaPosicoesZ.Item(i - 1) + 1
                End If
            Next
            Dim k As Integer = 0
            For i As Integer = 0 To (numeroPolilinhas2d - 1)
                Dim nx(tamanhoPolilinha2d(i)) As Double
                Dim ny(tamanhoPolilinha2d(i)) As Double
                Dim nz(tamanhoPolilinha2d(i)) As Double
                For j As Integer = 0 To (tamanhoPolilinha2d(i) - 1)
                    nx(j) = x(k)
                    ny(j) = y(k)
                    nz(j) = z(k)
                    k = k + 1
                Next
                funcoes.construirPolilinha2d(nx, ny, nz, tamanhoPolilinha2d(i))
                k = k - 1
            Next
        Else
            funcoes.construirPolilinha2d(x, y, z, tamanho)
        End If

    End Sub



End Module
