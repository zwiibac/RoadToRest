using Nancy;
using Nancy.TinyIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDisplay.Server
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        private readonly ITextApi _textApi;

        public Bootstrapper(ITextApi textApi)
        {
            _textApi = textApi;
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            container.Register(_textApi);
        }
    }
}
