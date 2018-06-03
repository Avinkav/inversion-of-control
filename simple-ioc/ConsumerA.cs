using System;

namespace dotnet_c_sharp
{
    public class ConsumerA
    {
        private readonly MathService _mathService;
        private string _secret;

        public ConsumerA(MathService serviceA){
            _mathService = serviceA;
            _secret = "";
        }
        
        public void UseMathService(int v){
           _mathService.Add(v);
        }


        public void SetMySecret(string v)
        {
            _secret =  v;
        }

        public void WhatsMySecret()
        {
            Console.WriteLine($"Consumer A says.... {_secret}\n");
        }
    }

}