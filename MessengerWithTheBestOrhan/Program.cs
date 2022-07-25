using System.Net;
using System.Net.Sockets;
using System.Text;


Socket listeningSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

try
{
    //Прослушиваем по адресу
    IPEndPoint localIP = new IPEndPoint(IPAddress.Parse("10.1.10.23"), 12001);
    listeningSocket.Bind(localIP);

    while (true)
    {
        // получаем сообщение
        StringBuilder builder = new StringBuilder();
        int bytes = 0; // количество полученных байтов
        byte[] data = new byte[256]; // буфер для получаемых данных

        //адрес, с которого пришли данные
        EndPoint remoteIp = new IPEndPoint(IPAddress.Any, 0);

        do
        {
            bytes = listeningSocket.ReceiveFrom(data, ref remoteIp);
            builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
        }
        while (listeningSocket.Available > 0);
        // получаем данные о подключении
        IPEndPoint remoteFullIp = remoteIp as IPEndPoint;

        // выводим сообщение
        Console.WriteLine("{0}:{1} - {2}", remoteFullIp.Address.ToString(), remoteFullIp.Port, builder.ToString());
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}



Socket listeningSocket;

try
{
    listeningSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    EndPoint remotePoint = new IPEndPoint(IPAddress.Parse("10.1.10.18"), 12001);

    // отправка сообщений на разные порты
    while (true)
    {
        string message = Console.ReadLine();

        byte[] data = Encoding.Unicode.GetBytes(message);
        listeningSocket.SendTo(data, remotePoint);
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
