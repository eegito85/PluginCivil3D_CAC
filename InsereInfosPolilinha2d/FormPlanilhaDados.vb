Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.Colors

Public Class FormPlanilhaDados

    Dim dados As String
    Dim lstStrMenor As New List(Of String)
    Dim lstStrSoma As New List(Of String)
    Dim valeta As String = ""

    Private Sub FormPlanilhaDados_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        dados = MyBase.Text
        MyBase.Text = "Dados da valeta"

        Dim mtzLinhas() As String = dados.Split("#")
        Dim nomeValeta As String = mtzLinhas(0)
        Dim numeroLinhas As Integer = mtzLinhas.Length - 1
        Dim x1s(numeroLinhas) As String
        Dim y1s(numeroLinhas) As String
        Dim z1s(numeroLinhas) As String
        Dim x2s(numeroLinhas) As String
        Dim y2s(numeroLinhas) As String
        Dim z2s(numeroLinhas) As String
        Dim exts(numeroLinhas) As String
        Dim dcls(numeroLinhas) As String
        Dim ext(numeroLinhas) As Double
        Dim dcl(numeroLinhas) As Double
        Dim dclA(numeroLinhas) As Double
        Dim temp(8) As String

        '-------------->>>>>> DADOS ORIGINAIS DA POLILINHA

        For i As Integer = 1 To numeroLinhas

            temp = mtzLinhas(i).Split("*")
            x1s(i - 1) = temp(0)
            y1s(i - 1) = temp(1)
            z1s(i - 1) = temp(2)
            x2s(i - 1) = temp(3)
            y2s(i - 1) = temp(4)
            z2s(i - 1) = temp(5)
            exts(i - 1) = temp(6)
            dcls(i - 1) = temp(7)
            ext(i - 1) = Convert.ToDouble(temp(6))
            dcl(i - 1) = Convert.ToDouble(temp(7))
            dclA(i - 1) = Math.Round(dcl(i - 1), 0)
            'Me.dgvDadosDesenho.Rows.Add(nomeValeta, numeroDecimal(temp(0)), numeroDecimal(temp(1)), numeroDecimal(temp(2)), numeroDecimal(temp(3)), numeroDecimal(temp(4)), numeroDecimal(temp(5)), numeroDecimal(temp(6)), numeroDecimal(temp(7)))
        Next

        '------------------>>>>> REAGRUPANDO AS DECLIVIDADES

        Dim lstX1 As New List(Of String)
        Dim lstY1 As New List(Of String)
        Dim lstZ1 As New List(Of String)
        Dim lstX2 As New List(Of String)
        Dim lstY2 As New List(Of String)
        Dim lstZ2 As New List(Of String)
        Dim indicador As Integer = 0
        Dim tamanho As Integer = 0
        Dim dx As Double = 0
        Dim dy As Double = 0
        Dim dz As Double = 0
        Dim distancia As Double = 0
        Dim declividade As Double = 0
        Dim novoTamanho As Integer = 0
        'Dim strFinal As String = ""

        For i As Integer = 0 To (numeroLinhas - 1)
            lstX1.Add(x1s(i))
            lstY1.Add(y1s(i))
            lstZ1.Add(z1s(i))
            lstX2.Add(x2s(i))
            lstY2.Add(y2s(i))
            lstZ2.Add(z2s(i))
            indicador = 0
            If (i = 0) Then
                If (numeroLinhas > 1) Then
                    For j As Integer = 0 To (numeroLinhas - 1)
                        If (j > i) Then
                            If ((dclA(i) = dclA(j)) And (indicador = 0)) Then
                                lstX1.Add(x1s(j))
                                lstY1.Add(y1s(j))
                                lstZ1.Add(z1s(j))
                                lstX2.Add(x2s(j))
                                lstY2.Add(y2s(j))
                                lstZ2.Add(z2s(j))
                            Else
                                indicador = 1
                            End If
                        End If
                    Next
                End If
                tamanho = lstX1.Count
                dx = (Convert.ToDouble(lstX2.Item(tamanho - 1)) - Convert.ToDouble(lstX1.Item(0))) * (Convert.ToDouble(lstX2.Item(tamanho - 1)) - Convert.ToDouble(lstX1.Item(0)))
                dy = (Convert.ToDouble(lstY2.Item(tamanho - 1)) - Convert.ToDouble(lstY1.Item(0))) * (Convert.ToDouble(lstY2.Item(tamanho - 1)) - Convert.ToDouble(lstY1.Item(0)))
                dz = (Convert.ToDouble(lstZ2.Item(tamanho - 1)) - Convert.ToDouble(lstZ1.Item(0))) * (Convert.ToDouble(lstZ2.Item(tamanho - 1)) - Convert.ToDouble(lstZ1.Item(0)))
                distancia = Math.Round(Math.Sqrt(dx + dy + dz), 2)
                declividade = Math.Abs(Math.Round(((100 * (Convert.ToDouble(lstZ2.Item(tamanho - 1)) - Convert.ToDouble(lstZ1.Item(0)))) / (Math.Sqrt(dx + dy))), 4))
                'If (dcl(0) < 0) Then
                '    declividade = 0 - declividade
                'End If
                Me.dgvDadosDesenho.Rows.Add(nomeValeta, numeroDecimal(lstX1.Item(0)), numeroDecimal(lstY1.Item(0)), numeroDecimal(lstZ1.Item(0)), numeroDecimal(lstX2.Item(tamanho - 1)), numeroDecimal(lstY2.Item(tamanho - 1)), numeroDecimal(lstZ2.Item(tamanho - 1)), numeroDecimal(Convert.ToString(distancia)), numeroDecimal(Convert.ToString(declividade)))
                novoTamanho = novoTamanho + 1
            End If
            If ((i > 0) And (i <> numeroLinhas - 1)) Then
                If (dclA(i) <> dclA(i - 1)) Then
                    For j As Integer = 0 To (numeroLinhas - 1)
                        If (j > i) Then
                            If ((dclA(i) = dclA(j)) And (indicador = 0)) Then
                                lstX1.Add(x1s(j))
                                lstY1.Add(y1s(j))
                                lstZ1.Add(z1s(j))
                                lstX2.Add(x2s(j))
                                lstY2.Add(y2s(j))
                                lstZ2.Add(z2s(j))
                            Else
                                indicador = 1
                            End If
                        End If
                    Next
                    tamanho = lstX1.Count
                    dx = (Convert.ToDouble(lstX2.Item(tamanho - 1)) - Convert.ToDouble(lstX1.Item(0))) * (Convert.ToDouble(lstX2.Item(tamanho - 1)) - Convert.ToDouble(lstX1.Item(0)))
                    dy = (Convert.ToDouble(lstY2.Item(tamanho - 1)) - Convert.ToDouble(lstY1.Item(0))) * (Convert.ToDouble(lstY2.Item(tamanho - 1)) - Convert.ToDouble(lstY1.Item(0)))
                    dz = (Convert.ToDouble(lstZ2.Item(tamanho - 1)) - Convert.ToDouble(lstZ1.Item(0))) * (Convert.ToDouble(lstZ2.Item(tamanho - 1)) - Convert.ToDouble(lstZ1.Item(0)))
                    distancia = Math.Round(Math.Sqrt(dx + dy + dz), 2)
                    declividade = Math.Abs(Math.Round(((100 * (Convert.ToDouble(lstZ2.Item(tamanho - 1)) - Convert.ToDouble(lstZ1.Item(0)))) / (Math.Sqrt(dx + dy))), 4))
                    'If (dcl(0) < 0) Then
                    '    declividade = 0 - declividade
                    'End If
                    Me.dgvDadosDesenho.Rows.Add(nomeValeta, numeroDecimal(lstX1.Item(0)), numeroDecimal(lstY1.Item(0)), numeroDecimal(lstZ1.Item(0)), numeroDecimal(lstX2.Item(tamanho - 1)), numeroDecimal(lstY2.Item(tamanho - 1)), numeroDecimal(lstZ2.Item(tamanho - 1)), numeroDecimal(Convert.ToString(distancia)), numeroDecimal(Convert.ToString(declividade)))
                    novoTamanho = novoTamanho + 1
                End If
            End If
            If ((i = numeroLinhas - 1) And (numeroLinhas > 1)) Then
                If (dclA(i) <> dclA(i - 1)) Then
                    tamanho = lstX1.Count
                    dx = (Convert.ToDouble(lstX2.Item(tamanho - 1)) - Convert.ToDouble(lstX1.Item(0))) * (Convert.ToDouble(lstX2.Item(tamanho - 1)) - Convert.ToDouble(lstX1.Item(0)))
                    dy = (Convert.ToDouble(lstY2.Item(tamanho - 1)) - Convert.ToDouble(lstY1.Item(0))) * (Convert.ToDouble(lstY2.Item(tamanho - 1)) - Convert.ToDouble(lstY1.Item(0)))
                    dz = (Convert.ToDouble(lstZ2.Item(tamanho - 1)) - Convert.ToDouble(lstZ1.Item(0))) * (Convert.ToDouble(lstZ2.Item(tamanho - 1)) - Convert.ToDouble(lstZ1.Item(0)))
                    distancia = Math.Round(Math.Sqrt(dx + dy + dz), 2)
                    declividade = Math.Abs(Math.Round(((100 * (Convert.ToDouble(lstZ2.Item(tamanho - 1)) - Convert.ToDouble(lstZ1.Item(0)))) / (Math.Sqrt(dx + dy))), 4))
                    'If (dcl(0) < 0) Then
                    '    declividade = 0 - declividade
                    'End If
                    Me.dgvDadosDesenho.Rows.Add(nomeValeta, numeroDecimal(lstX1.Item(0)), numeroDecimal(lstY1.Item(0)), numeroDecimal(lstZ1.Item(0)), numeroDecimal(lstX2.Item(tamanho - 1)), numeroDecimal(lstY2.Item(tamanho - 1)), numeroDecimal(lstZ2.Item(tamanho - 1)), numeroDecimal(Convert.ToString(distancia)), numeroDecimal(Convert.ToString(declividade)))
                    novoTamanho = novoTamanho + 1
                End If
            End If
            lstX1.Clear()
            lstY1.Clear()
            lstZ1.Clear()
            lstX2.Clear()
            lstY2.Clear()
            lstZ2.Clear()
        Next

        'MsgBox(novoTamanho)
        Dim qtdLinhas As Integer = novoTamanho
        Dim qtdColunas As Integer = 9
        Dim qtdPontos As Integer = qtdLinhas + 1
        Dim x(qtdPontos) As Double
        Dim y(qtdPontos) As Double
        Dim z(qtdPontos) As Double
        Dim elementos(qtdLinhas, qtdColunas) As Double

        For i As Integer = 0 To (qtdLinhas - 1)
            If (i = (qtdLinhas - 1)) Then
                x(i) = Convert.ToDouble(inverteNumDecimal(dgvDadosDesenho.Item(1, i).Value))
                y(i) = Convert.ToDouble(inverteNumDecimal(dgvDadosDesenho.Item(2, i).Value))
                z(i) = Convert.ToDouble(inverteNumDecimal(dgvDadosDesenho.Item(3, i).Value))
                x(i + 1) = Convert.ToDouble(inverteNumDecimal(dgvDadosDesenho.Item(4, i).Value))
                y(i + 1) = Convert.ToDouble(inverteNumDecimal(dgvDadosDesenho.Item(5, i).Value))
                z(i + 1) = Convert.ToDouble(inverteNumDecimal(dgvDadosDesenho.Item(6, i).Value))
            Else
                x(i) = Convert.ToDouble(inverteNumDecimal(dgvDadosDesenho.Item(1, i).Value))
                y(i) = Convert.ToDouble(inverteNumDecimal(dgvDadosDesenho.Item(2, i).Value))
                z(i) = Convert.ToDouble(inverteNumDecimal(dgvDadosDesenho.Item(3, i).Value))
            End If
        Next

        'For i As Integer = 0 To (qtdPontos - 1)
        '    MsgBox(x(i).ToString & " " & y(i).ToString & " " & z(i).ToString)
        'Next

        'constroiPolilinha3d(x, y, z, qtdPontos)

    End Sub

    Public Function numeroDecimal(ByVal numero As String) As String

        Dim resultado As String = Replace(numero, ".", ",")

        Return resultado

    End Function


    Public Function inverteNumDecimal(ByVal numero As String) As String

        Dim resultado As String = Replace(numero, ",", ".")

        Return resultado

    End Function

    Public Sub constroiPolilinha3d(ByVal x() As Double, ByVal y() As Double, ByVal z() As Double, ByVal tamanho As Integer)

        Dim pt As Point3d = New Point3d(0, 0, 0)
        Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument
        Dim acCurDb As Database = acDoc.Database

        Using acTrans As Transaction = acCurDb.TransactionManager.StartTransaction()

            Dim acBlkTbl As BlockTable
            acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead)

            Dim acBlkTblRec As BlockTableRecord
            acBlkTblRec = acTrans.GetObject(acBlkTbl(BlockTableRecord.ModelSpace), OpenMode.ForWrite)
            Dim acPoly As Polyline3d = New Polyline3d()
            acBlkTblRec.AppendEntity(acPoly)
            acTrans.AddNewlyCreatedDBObject(acPoly, True)
            acPoly.SetDatabaseDefaults()
            Dim acLyrTbl As LayerTable
            acLyrTbl = acTrans.GetObject(acCurDb.LayerTableId, OpenMode.ForRead)
            Dim cor As Color
            cor = Color.FromColorIndex(ColorMethod.ByAci, 3)
            Dim sLayerName As String = "layerNovaPolilinha3D"
            If acLyrTbl.Has(sLayerName) = False Then
                Dim acLyrTblRec As LayerTableRecord = New LayerTableRecord()
                acLyrTblRec.LineWeight = LineWeight.LineWeight015
                acLyrTblRec.Color = Color.FromColorIndex(ColorMethod.ByLayer, 3)
                acLyrTblRec.Name = sLayerName
                acLyrTbl.UpgradeOpen()
                acLyrTbl.Add(acLyrTblRec)
                acTrans.AddNewlyCreatedDBObject(acLyrTblRec, True)
            End If


            For i As Integer = 0 To (tamanho - 1)
                pt = New Point3d(x(i), y(i), z(i))
                Dim vex3d As PolylineVertex3d = New PolylineVertex3d(pt)
                acPoly.AppendVertex(vex3d)
                acTrans.AddNewlyCreatedDBObject(vex3d, True)
            Next

            acPoly.Layer = sLayerName
            acCurDb.Clayer = acLyrTbl(sLayerName)

            acTrans.Commit()
        End Using

    End Sub

    Private Sub btOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btOK.Click

        Me.Hide()




        'Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument
        'Dim pKeyOpts As PromptKeywordOptions = New PromptKeywordOptions("")
        'pKeyOpts.Message = vbLf & "Deseja inserir a tabela no desenho? <S/N> "
        'pKeyOpts.Keywords.Add("s")
        'pKeyOpts.Keywords.Add("n")
        'pKeyOpts.AllowNone = False
        'Dim pKeyRes As PromptResult = acDoc.Editor.GetKeywords(pKeyOpts)
        '
        'If (pKeyRes.StringResult = "s") Then
        '    Dim pontoClicado As Point3d = funcoes.SelecionarPonto
        '    Dim x As Double = pontoClicado.X
        '    Dim y As Double = pontoClicado.Y
        '    Dim pontoInsercao As Point3d = New Point3d(0, 0, 0)
        '    funcoes.constroiCabecalho(pontoClicado, "Nome da Valeta", "Extensão(m)", "Declividade(%)")
        '    Dim tamanho As Integer = lstStrMenor.Count
        '    'MsgBox(tamanho)
        '    If (tamanho <> 0) Then
        '        For i As Integer = 0 To (tamanho - 1)
        '            y = y - 3.5
        '            pontoInsercao = New Point3d(x, y, 0)
        '            funcoes.constroiLinhaTabela(pontoInsercao, valeta, lstStrSoma.Item(i), lstStrMenor.Item(i))
        '        Next
        '    End If
        'End If

    End Sub




End Class