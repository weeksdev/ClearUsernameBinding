using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;


/*
 * Want more WCF tips?
 * Visit http://webservices20.blogspot.com/
 */


namespace WebServices20.BindingExtenions
{
    public class ClearUsernameBinding : CustomBinding
    {
        private MessageVersion messageVersion = MessageVersion.None;

        public void SetMessageVersion(MessageVersion value)
        {
            this.messageVersion = value;
        }

        public override BindingElementCollection CreateBindingElements()
        {
            var res = new BindingElementCollection();
            res.Add(new TextMessageEncodingBindingElement() { MessageVersion = this.messageVersion});
            res.Add(SecurityBindingElement.CreateUserNameOverTransportBindingElement());
            res.Add(new AutoSecuredHttpTransportElement());
            //removing timestamp element because of issues with non .net web services
            //http://kjellsj.blogspot.com/2008/03/wcf-ws-security-ssl-transportwithmessag.html
            res.Find<SecurityBindingElement>().IncludeTimestamp = false;
            return res;
        }

        public override string Scheme
        {
            get
            {
                return "http";
            }
        }
    }
}
