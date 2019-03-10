using Nancy;
using Nancy.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDisplay.Server
{
    public class TextModule : NancyModule
    {
        private readonly ITextApi _textApi;

        public TextModule(ITextApi textApi)
        {
            _textApi = textApi;

            Get["/text"] = _ =>
            {
                _textApi.Status = $"GET: {Request.Url}";
                return _textApi.Content;
            };

            Delete["/text"] = _ =>
            {
                _textApi.Status = $"DELETE: {Request.Url}";
                _textApi.Clear();
                return string.Empty;
            };

            Get["/text/{line}"] = parameters =>
            {
                _textApi.Status = $"GET: {Request.Url}";
                return _textApi.GetLine(parameters.line);
            };

            Put["/text/{line}"] = parameters =>
            {
                _textApi.Status = $"PUT: {Request.Url}";
                _textApi.SetLine(parameters.line, Request.Body.AsString());
                return string.Empty;
            };

            Put["/text/newline"] = _ =>
            {
                _textApi.Status = $"PUT: {Request.Url}";
                _textApi.AppendLine(Request.Body.AsString());
                return string.Empty;
            };
        }
    }
}
