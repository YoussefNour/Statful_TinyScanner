using System;

namespace ConsoleApp2
{
    class Token
    {
        public enum tokentypes { reserved_word, special_symbol, identifier, number, comment };
        private string stringval;
        private tokentypes type;

        public Token(string val, tokentypes type)
        {
            stringval = val;
            this.type = type;
        }
        public Token()
        {

        }
        public void setval(string value)
        {
            stringval = value;
        }
        public void settype(tokentypes type)
        {
            this.type = type;
        }
        public void printtoken()
        {
            switch(type)
            {
                case tokentypes.comment:
                    Console.WriteLine(stringval + "\t-->Comment");
                    break;
                case tokentypes.identifier:
                    Console.WriteLine(stringval + "\t-->Identifier");
                    break;
                case tokentypes.number:
                    Console.WriteLine(stringval + "\t-->Number");
                    break;
                case tokentypes.reserved_word:
                    Console.WriteLine(stringval + "\t-->Reserved Word");
                    break;
                case tokentypes.special_symbol:
                    Console.WriteLine(stringval + "\t-->Special Symbol");
                    break;
            }
        }
    }
}
