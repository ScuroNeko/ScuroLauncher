using System.Net;
using System.Net.Security;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;

namespace ScuroLauncher;

public class ProxyService
{
    private static readonly string[] RedirectDomains = [".yuanshen.com", ".bhsr.com", ".starrails.com", ".hoyoverse.com", ".mihoyo.com"];

    private readonly ProxyServer _server;
    private readonly ExplicitProxyEndPoint _endPoint;

    public string RedirectUrl = "http://127.0.0.1:8888/";

    public ProxyService()
    {
        _server = new ProxyServer();
        _server.CertificateManager.EnsureRootCertificate();

        _server.BeforeRequest += BeforeRequest;
        _server.ServerCertificateValidationCallback += OnCertValidation;

        _endPoint = new ExplicitProxyEndPoint(IPAddress.Any, 8080);
        _endPoint.BeforeTunnelConnectRequest += BeforeTunnelConnectRequest;

        _server.AddEndPoint(_endPoint);
        _server.Start();
        Providers.Logger.Info("ProxyService loaded and ready to work");
    }

    ~ProxyService()
    {
        _server.BeforeRequest -= BeforeRequest;
        _server.ServerCertificateValidationCallback -= OnCertValidation;
        _endPoint.BeforeTunnelConnectRequest -= BeforeTunnelConnectRequest;
        _server.Dispose();
        _server.Stop();
    }

    public void Start()
    {
        _server.SetAsSystemHttpProxy(_endPoint);
        _server.SetAsSystemHttpsProxy(_endPoint);
        Providers.Logger.Info("ProxyService was started");
    }

    public void Stop()
    {
        _server.DisableSystemHttpProxy();
        _server.DisableSystemHttpsProxy();
        Providers.Logger.Info("ProxyService was stopped");
    }

    private Task BeforeTunnelConnectRequest(object sender, TunnelConnectSessionEventArgs args)
    {
        var hostname = args.HttpClient.Request.RequestUri.Host;
        args.DecryptSsl = ShouldRedirect(hostname);

        return Task.CompletedTask;
    }

    private Task OnCertValidation(object sender, CertificateValidationEventArgs args)
    {
        if (args.SslPolicyErrors == SslPolicyErrors.None) args.IsValid = true;
        return Task.CompletedTask;
    }

    private Task BeforeRequest(object sender, SessionEventArgs args)
    {
        var hostname = args.HttpClient.Request.RequestUri.Host;

        if (!ShouldRedirect(hostname)) return Task.CompletedTask;
        var requestUrl = args.HttpClient.Request.Url;
        Uri local = new(RedirectUrl);

        var replacedUrl = new UriBuilder(requestUrl)
        {
            Scheme = local.Scheme,
            Host = local.Host,
            Port = local.Port
        }.Uri.ToString();

        replacedUrl = replacedUrl.Replace("hk4e_cn", "hk4e_global"); // cn -> global for CN builds
        args.HttpClient.Request.Url = replacedUrl;

        Providers.Logger.Info($"Redirecting: {replacedUrl}");

        return Task.CompletedTask;
    }

    private static bool ShouldRedirect(string hostname) => RedirectDomains.Any(hostname.EndsWith);
}