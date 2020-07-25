Imports System.Net.Sockets

Module Module1

    Sub Main()

        Console.WriteLine("Start listening on port 456, will read first 4096 bytes and print then with utf8 encoding")

        Dim tcplistener As New TcpListener(Net.IPAddress.Any, 456)
        tcplistener.Start()


        Do
            Dim tcpclient As TcpClient = tcplistener.AcceptTcpClient
            Dim ns As NetworkStream = tcpclient.GetStream


            Threading.Thread.Sleep(1000)

            Dim buff(4095) As Byte
            Dim c As Integer = ns.Read(buff, 0, 4096)

            Dim Request As String = System.Text.Encoding.UTF8.GetString(buff.Take(c).ToArray)

            Console.WriteLine("Received request on " & Now)
            Console.WriteLine(Request)

            Dim Response As String = "HTTP/1.0 200 OK" & vbCrLf & vbCrLf & Request
            Dim ResponseBuff As Byte() = System.Text.Encoding.UTF8.GetBytes(Response)
            ns.Write(ResponseBuff, 0, ResponseBuff.Count)


            ns.Close()
            tcpclient.Close()
        Loop

    End Sub

End Module
