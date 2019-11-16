﻿Imports System.IO
Imports System.Threading

Public Class frmNearFarHedgingSettings

    Private _cts As CancellationTokenSource = Nothing
    Private _NearFarHedgingSettings As NearFarHedgingUserInputs = Nothing
    Private _NearFarHedgingSettingsFilename As String = Path.Combine(My.Application.Info.DirectoryPath, "NearFarHedgingSettings.Strategy.a2t")

    Public Sub New(ByRef userInputs As NearFarHedgingUserInputs)
        InitializeComponent()
        _NearFarHedgingSettings = userInputs
    End Sub

    Private Sub frmNearFarHedgingSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSettings()
    End Sub

    Private Sub btnSaveNearFarHedgingSettings_Click(sender As Object, e As EventArgs) Handles btnSaveNearFarHedgingSettings.Click
        Try
            _cts = New CancellationTokenSource
            If _NearFarHedgingSettings Is Nothing Then _NearFarHedgingSettings = New NearFarHedgingUserInputs
            _NearFarHedgingSettings.InstrumentsData = Nothing
            ValidateInputs()
            SaveSettings()
            Me.Close()
        Catch ex As Exception
            MsgBox(String.Format("The following error occurred: {0}", ex.Message), MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub LoadSettings()
        If File.Exists(_NearFarHedgingSettingsFilename) Then
            _NearFarHedgingSettings = Utilities.Strings.DeserializeToCollection(Of NearFarHedgingUserInputs)(_NearFarHedgingSettingsFilename)
            txtSignalTimeFrame.Text = _NearFarHedgingSettings.SignalTimeFrame
            dtpckrTradeStartTime.Value = _NearFarHedgingSettings.TradeStartTime
            dtpckrLastTradeEntryTime.Value = _NearFarHedgingSettings.LastTradeEntryTime
            dtpckrEODExitTime.Value = _NearFarHedgingSettings.EODExitTime
            txtMaxLossPercentagePerDay.Text = _NearFarHedgingSettings.MaxLossPerDay
            txtMaxProfitPercentagePerDay.Text = _NearFarHedgingSettings.MaxProfitPerDay
            txtBollingerPeriod.Text = _NearFarHedgingSettings.BollingerPeriod
            txtBollingerMultiplier.Text = _NearFarHedgingSettings.BollingerMultiplier
            txtInstrumentDetalis.Text = _NearFarHedgingSettings.InstrumentDetailsFilePath
            If _NearFarHedgingSettings.UseBothSignal Then
                rdbBoth.Checked = True
            Else
                rdbAnyOne.Checked = True
            End If
            txtTelegramAPI.Text = _NearFarHedgingSettings.TelegramAPIKey
            txtTelegramChatID.Text = _NearFarHedgingSettings.TelegramChatID
            txtTelegramChatIDForPL.Text = _NearFarHedgingSettings.TelegramPLChatID
        End If
    End Sub
    Private Sub SaveSettings()
        _NearFarHedgingSettings.SignalTimeFrame = txtSignalTimeFrame.Text
        _NearFarHedgingSettings.TradeStartTime = dtpckrTradeStartTime.Value
        _NearFarHedgingSettings.LastTradeEntryTime = dtpckrLastTradeEntryTime.Value
        _NearFarHedgingSettings.EODExitTime = dtpckrEODExitTime.Value
        _NearFarHedgingSettings.MaxLossPerDay = txtMaxLossPercentagePerDay.Text
        _NearFarHedgingSettings.MaxProfitPerDay = txtMaxProfitPercentagePerDay.Text
        _NearFarHedgingSettings.BollingerPeriod = txtBollingerPeriod.Text
        _NearFarHedgingSettings.BollingerMultiplier = txtBollingerMultiplier.Text
        _NearFarHedgingSettings.InstrumentDetailsFilePath = txtInstrumentDetalis.Text
        If rdbAnyOne.Checked Then
            _NearFarHedgingSettings.UseBothSignal = False
        ElseIf rdbBoth.Checked Then
            _NearFarHedgingSettings.UseBothSignal = True
        End If
        _NearFarHedgingSettings.TelegramAPIKey = txtTelegramAPI.Text
        _NearFarHedgingSettings.TelegramChatID = txtTelegramChatID.Text
        _NearFarHedgingSettings.TelegramPLChatID = txtTelegramChatIDForPL.Text

        Utilities.Strings.SerializeFromCollection(Of NearFarHedgingUserInputs)(_NearFarHedgingSettingsFilename, _NearFarHedgingSettings)
    End Sub
    Private Function ValidateNumbers(ByVal startNumber As Decimal, ByVal endNumber As Decimal, ByVal inputTB As TextBox) As Boolean
        Dim ret As Boolean = False
        If IsNumeric(inputTB.Text) Then
            If Val(inputTB.Text) >= startNumber And Val(inputTB.Text) <= endNumber Then
                ret = True
            End If
        End If
        If Not ret Then Throw New ApplicationException(String.Format("{0} cannot have a value < {1} or > {2}", inputTB.Tag, startNumber, endNumber))
        Return ret
    End Function
    Private Sub ValidateFile()
        _NearFarHedgingSettings.FillInstrumentDetails(txtInstrumentDetalis.Text, _cts)
    End Sub
    Private Sub ValidateInputs()
        ValidateNumbers(1, 60, txtSignalTimeFrame)
        ValidateNumbers(0, 100, txtMaxLossPercentagePerDay)
        ValidateNumbers(0, 100, txtMaxProfitPercentagePerDay)
        ValidateNumbers(1, Integer.MaxValue, txtBollingerMultiplier)
        ValidateNumbers(1, Integer.MaxValue, txtBollingerPeriod)
        ValidateFile()
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        opnFileSettings.Filter = "|*.csv"
        opnFileSettings.ShowDialog()
    End Sub

    Private Sub opnFileSettings_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles opnFileSettings.FileOk
        Dim extension As String = Path.GetExtension(opnFileSettings.FileName)
        If extension = ".csv" Then
            txtInstrumentDetalis.Text = opnFileSettings.FileName
        Else
            MsgBox("File Type not supported. Please Try again.", MsgBoxStyle.Critical)
        End If
    End Sub

End Class