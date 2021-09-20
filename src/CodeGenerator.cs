using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace MyServer
{
    class CodeGenerator  : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            byte[] input = e.RawData;

            // pop the last value for length out of byte array
            int length = (int)input[input.Length - 1];
            Array.Resize(ref input, input.Length - 1);

            // convert byte array leftovers into number of codes
            Array.Resize(ref input, 4);
            int nrOfCodes = BitConverter.ToInt32(input, 0);

            // read xml file for stored codes, generate new ones, save them into xml
            string[] codeArray = GenerateCodes(XMLService.ReadXML<string>(), length, nrOfCodes);
            XMLService.WriteXML<string[]>(codeArray);

            byte[] output = new byte[1] { 0 };
            Send(output);
        }

        private string[] GenerateCodes(string[] storedCodes, int length, int nrOfCodes)
        {
            int arraySize = nrOfCodes + storedCodes.Length;

            string[] generatedCodes = new string[arraySize];
;
            Array.Copy(storedCodes, generatedCodes, storedCodes.Length);

            int generatedCodesCount = 0;
            while(generatedCodesCount != nrOfCodes)
            {
                string code = GenerateCode(length);

                while (generatedCodes.Contains(code))
                {
                    code = GenerateCode(length);
                }

                generatedCodes[generatedCodesCount + storedCodes.Length] = code;
                generatedCodesCount++;
            }

            return generatedCodes;
        }

        private string GenerateCode(int length)
        {
            Random rnd = new Random();
            string code = "";
            
            for (int i = 0; i < length; i++)
            {
                code = String.Concat(code, rnd.Next(0, 9).ToString());
            }

            return code;
        }
    }
}
