
Imports System
Imports System.Collections.Generic

Partial Public Class InfoFrais
    Public Property Id As Long
    Public Property GrilleId As Long
    Public Property BornInf As Decimal
    Public Property BornSup As Decimal
    Public Property Frais As Decimal
    Public Property Taux As Decimal

    Public Overridable Property Grille As Grille

End Class

