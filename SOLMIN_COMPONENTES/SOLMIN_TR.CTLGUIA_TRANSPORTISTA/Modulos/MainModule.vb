Module MainModule

    'Declaración de la API (Para Liberar Memoria http://gdev.wordpress.com/2005/11/30/liberar-memoria-con-vb-net/)
    Private Declare Auto Function SetProcessWorkingSetSize Lib "kernel32.dll" (ByVal procHandle As IntPtr, ByVal min As Int32, ByVal max As Int32) As Boolean

    'Funcion de liberacion de memoria
    Public Sub ClearMemory()

        Try

            ''Forzando al Garbage Collector
            GC.Collect()
            GC.WaitForPendingFinalizers()
            Dim Mem As Process
            Mem = Process.GetCurrentProcess()
            SetProcessWorkingSetSize(Mem.Handle, -1, -1)

        Catch : End Try

    End Sub

End Module
