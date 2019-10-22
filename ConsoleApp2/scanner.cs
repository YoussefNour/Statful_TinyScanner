using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp2
{
    class scanner
    {
        private string[] reservedWords = { "if", "then", "else", "end", "repeat", "until", "read", "write" };
        private char[] specialsymbol = { '+', '-', '=','*', '/', '<','>', '(', ')', ';'};
        protected enum states { START,COMMENT,INNUM,INID,INASSIGN };
        protected states state;
        protected List<Token> tokens;

        public List<Token> GetTokens(string path)
        {
            tokens = new List<Token>();
            StreamReader sr = new StreamReader(path);
            StringBuilder code = new StringBuilder();
            code.Append(sr.ReadToEnd());
            string word=null;
            state = states.START;

            for(int g=0; g<code.Length;g++)
            {
                char c = code[g];
                if(state == states.COMMENT)
                {
                    if(c =='}')
                    {
                        word += c;
                        Token t = new Token(word, Token.tokentypes.comment);
                        tokens.Add(t);
                        word = null;
                        state = states.START;
                    }
                    else
                    {
                        word += c;
                        continue;
                    }
                }
                if(state == states.INID)
                {
                    if(char.IsLetterOrDigit(c))
                    {
                        word += c;
                        continue;
                    }
                    else
                    {
                        if (Isreserved(word))
                        {
                            Token t = new Token(word, Token.tokentypes.reserved_word);
                            tokens.Add(t);
                            word = null;
                            state = states.START;
                        }
                        else
                        {
                            Token t = new Token(word, Token.tokentypes.identifier);
                            tokens.Add(t);
                            word = null;
                            state = states.START;
                        }
                    }
                }
                if(state == states.INNUM)
                {
                    if (!char.IsDigit(c))
                    {
                        Token t = new Token(word, Token.tokentypes.number);
                        tokens.Add(t);
                        word = null;
                        state = states.START;
                        if (IsspecialSymbol(c))
                        {
                            word += c;
                            Token v = new Token(word, Token.tokentypes.special_symbol);
                            tokens.Add(v);
                            word = null;
                        }
                    }
                    else
                    {
                        word += c;
                        continue;
                    }
                }
                if(state==states.INASSIGN)
                {
                    if(c == '=')
                    {
                        word += c;
                        Token t = new Token(word, Token.tokentypes.special_symbol);
                        tokens.Add(t);
                        word = null;
                        state = states.START;
                    }
                    else
                    {
                        Console.WriteLine("assign symbol error");
                        state = states.START;
                    }
                }
                if(state == states.START)
                {
                    if (c == '{')
                    {
                        state = states.COMMENT;
                        word += c;
                        continue;
                    }
                    if (char.IsLetter(c))
                    {
                        state = states.INID;
                        word += c;
                        continue;
                    }
                    if (char.IsDigit(c))
                    {
                        state = states.INNUM;
                        word += c;
                        continue;
                    }
                    if (c == ':')
                    {
                        state = states.INASSIGN;
                        word += c;
                        continue;
                    }
                    if (IsspecialSymbol(c))
                    {
                        word += c;
                        Token t = new Token(word, Token.tokentypes.special_symbol);
                        tokens.Add(t);
                        word = null;
                        continue;
                    }
                }
            }
            return tokens;
        }
        private bool IsspecialSymbol(char c)
        {
            for(int i =0;i<specialsymbol.Length;i++)
            {
                if (c == specialsymbol[i])
                {
                    return true;
                }
            }
            return false;
        }
        private bool Isreserved(string c)
        {
            for (int i = 0; i < reservedWords.Length; i++)
            {
                if (c == reservedWords[i])
                {
                    return true;
                }
            }
            return false;
        }
    }
}
