﻿var lookup = new Dictionary<char, string>
{
    ['0'] = "0000", ['1'] = "0001", ['2'] = "0010", ['3'] = "0011", ['4'] = "0100", ['5'] = "0101", ['6'] = "0110", ['7'] = "0111",
    ['8'] = "1000", ['9'] = "1001", ['A'] = "1010", ['B'] = "1011", ['C'] = "1100", ['D'] = "1101", ['E'] = "1110", ['F'] = "1111"
};

var stream = File.OpenText("input-test.txt");

while (stream.Peek() >= 0)
{
    var c = (char) stream.Read();
    Console.Write(lookup[c]);
}

Console.WriteLine();

enum ReadingState
{
    Initial,
    PacketType,
    PacketVersion,
    LiteralNumber,
    Operation,
    LengthTypeId
}