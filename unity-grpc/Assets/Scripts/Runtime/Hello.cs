using Grpc.Core;
using Grpc.Net.Client.Web;
using Grpc.Net.Client;
using System.Net.Http;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Helloworld;

public class Hello : MonoBehaviour
{
    public Button Button;

    public TMP_Text Text;

    public string host = "http://localhost:8080";

    private GrpcChannel channel;
    private Greeter.GreeterClient client;

    void Start()
    {
        Button.onClick.AddListener(OnHello);
        var options = new GrpcChannelOptions();
        var handler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());
        options.HttpHandler = handler;
        options.Credentials = ChannelCredentials.Insecure;
        channel = GrpcChannel.ForAddress(host, options);
        client = new Greeter.GreeterClient(channel);
    }
    private void OnDestroy()
    {
        channel.Dispose();
    }

    public void OnHello()
    {
        var reply = client.SayHello(new HelloRequest { Name = "unity" });
        Debug.Log(reply.ToString());
        Text.text = $"[{DateTime.Now}] {reply}";
    }
}