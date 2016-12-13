using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alta.INetwork
{
    public enum TypeClient
    {
        OTHER = -1, None = 0, TOUCH = 1, DISPLAY = 2, CLIENT = 3, CLIENT2 = 4, CLIENT3 = 5, CLIENT4 = 6, CLIENT5 = 7, CLIENT6 = 8, CLIENT7 = 9, CLIENT8 = 10,
        Player1, Player2, Player3, Player4, Player5, Player6, Player7, Player8, Player9, Player10
    }

    public interface IClient
    {
        TypeClient Type { get; }
        string Code { get; }
    }
}
