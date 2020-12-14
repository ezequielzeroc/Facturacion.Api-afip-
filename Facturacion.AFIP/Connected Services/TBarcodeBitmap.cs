using System;
using System.Collections.Generic;
using System.Drawing;

namespace FEAFIPLib
{
    public class TBarcodeBitmap
    {
        private static string digitoVerificador(string codigo)
        {
            int totalpares;
            int totalimpares; 
            int digito;

            totalimpares = 0; 
            for (int x = 0; x < codigo.Length; x+=2){
                totalimpares = totalimpares + Byte.Parse(codigo.Substring(x,1)); 
            }
            totalimpares = totalimpares * 3; 


            totalpares = 0; 
            for (int x = 1; x < codigo.Length; x+=2){ 
                totalpares = totalpares + Byte.Parse(codigo.Substring(x,1)); 
            }

            int xx = totalimpares + totalpares; 
            digito = 0; 

            while ((int)(xx / 10) * 10 != xx) {
              xx = xx + 1; 
              digito = digito + 1; 
            } 

            return  digito.ToString();
        }

        public static string generarCodigoBarras(Int64 cuit, Byte tipoComprobante, Byte puntoVenta, string cae, DateTime fechaVencimiento, float basewidth, float height, string archivo){
            string code = cuit.ToString("00000000000") + tipoComprobante.ToString("00") + puntoVenta.ToString("0000") + cae + fechaVencimiento.ToString("yyyyMMdd");
            code = code + digitoVerificador(code);
            float xpos = 0;
            float ypos = 0; 
            Bitmap flag = new Bitmap(6000, 1000);
            Graphics flagGraphics = Graphics.FromImage(flag);

            Brush brush = new SolidBrush(Color.White);
            flagGraphics.FillRectangle(brush, 0, 0, flag.Width, flag.Height);
            brush = new SolidBrush(Color.Black);
            //brush.Dispose();
            //Pen pen = new Pen(Color.Black);

            if (basewidth == 0)
            {
              basewidth = 2;
            }
            if (height == 0) {
              height = 30;
            }
    
            float wide = basewidth;
            float narrow = basewidth / 3;

            // wide/narrow codes for the digits
            IDictionary<string, string> barChar = new Dictionary<string, string>();
            barChar.Add("0", "nnwwn");
            barChar.Add("1", "wnnnw");
            barChar.Add("2", "nwnnw");
            barChar.Add("3", "wwnnn");
            barChar.Add("4", "nnwnw");
            barChar.Add("5", "wnwnn");
            barChar.Add("6", "nwwnn");
            barChar.Add("7", "nnnww");
            barChar.Add("8", "wnnwn");
            barChar.Add("9", "nwnwn");
            barChar.Add("A", "nn");
            barChar.Add("Z", "wn");

            // add leading zero if code-length is odd
            if (code.Length % 2 != 0) {
                code = "0" + code;
            }

            // add start and stop codes
            code = "AA" + code.ToLower() + "ZA";

            for (int i = 0; i < code.Length; i+=2){
                // choose next pair of digits
                string charBar = code.Substring(i, 1);
                string charSpace = code.Substring(i + 1, 1);
                // check whether it is a valid digit
                if (!barChar.ContainsKey(charBar)){
                    return "Caracter inválido en código de barras : " + charBar; 
                }
                if (!barChar.ContainsKey(charSpace)){
                    return "Caracter inválido en código de barras : " + charSpace;
                }
        
                // create a wide/narrow-sequence (first digit=bars, second digit=spaces)
                string seq = "";
                for (int s = 0; s < barChar[charBar].Length; s++){
                    seq = seq + barChar[charBar].Substring(s, 1) + barChar[charSpace].Substring(s, 1);
                }
                for (int bar = 0; bar < seq.Length; bar++)
                {
                    float lineWidth;
                    // set lineWidth depending on value
                    if (seq.Substring(bar, 1) == "n"){
                        lineWidth = narrow;
                    } else {
                        lineWidth = wide;
                    }
                    // draw every second value, because the second digit of the pair is represented by the spaces
                    if (bar % 2 == 0)
                    {
                        flagGraphics.FillRectangle(brush, xpos, ypos, lineWidth, height); 
                    }
                    xpos = xpos + lineWidth;
                }
            }

            RectangleF rect = new RectangleF(0, 0, xpos, height);
            flag = flag.Clone(rect, flag.PixelFormat);
            flag.Save(archivo);
            return code;
        }
        

    }
}
