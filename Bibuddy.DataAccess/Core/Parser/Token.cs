﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibuddy.DataAccess.Core.Parser
{
    internal struct Token
    {
        public TokenType Type;
        public string Value;

        public Token(TokenType type, string value = "")
        {
            Type = type;
            Value = value;
            Debug.WriteLine(type + "\t" + value);
        }
    }
        public enum TokenType
        {
            Start,

            Name,
            String,

            Quotation,

            LeftBrace,
            RightBrace,

            Equal,
            Comma,

            Concatenation,

            EOF
        }
    }
