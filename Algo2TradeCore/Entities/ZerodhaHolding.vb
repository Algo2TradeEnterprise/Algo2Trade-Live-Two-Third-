﻿Imports KiteConnect

Namespace Entities
    <Serializable>
    Public Class ZerodhaHolding
        Implements IHolding

        Public ReadOnly Property Product As String Implements IHolding.Product
            Get
                Return WrappedHolding.Product
            End Get
        End Property

        Public ReadOnly Property Price As Decimal Implements IHolding.Price
            Get
                Return WrappedHolding.Price
            End Get
        End Property

        Public ReadOnly Property AveragePrice As Decimal Implements IHolding.AveragePrice
            Get
                Return WrappedHolding.AveragePrice
            End Get
        End Property

        Public ReadOnly Property TradingSymbol As String Implements IHolding.TradingSymbol
            Get
                Return WrappedHolding.TradingSymbol
            End Get
        End Property

        Public ReadOnly Property T1Quantity As Integer Implements IHolding.T1Quantity
            Get
                Return WrappedHolding.T1Quantity
            End Get
        End Property

        Public ReadOnly Property InstrumentIdentifier As UInteger Implements IHolding.InstrumentIdentifier
            Get
                Return WrappedHolding.InstrumentToken
            End Get
        End Property

        Public ReadOnly Property ISIN As String Implements IHolding.ISIN
            Get
                Return WrappedHolding.ISIN
            End Get
        End Property

        Public ReadOnly Property RealisedQuantity As Integer Implements IHolding.RealisedQuantity
            Get
                Return WrappedHolding.RealisedQuantity
            End Get
        End Property

        Public ReadOnly Property Quantity As Integer Implements IHolding.Quantity
            Get
                Return WrappedHolding.Quantity
            End Get
        End Property

        <NonSerialized>
        Private _WrappedHolding As Holding
        Public Property WrappedHolding As Holding
            Get
                Return _WrappedHolding
            End Get
            Set(value As Holding)
                _WrappedHolding = value
            End Set
        End Property

        Public ReadOnly Property Broker As APISource Implements IHolding.Broker
            Get
                Return APISource.Zerodha
            End Get
        End Property
    End Class
End Namespace