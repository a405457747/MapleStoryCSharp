using System.Collections.Generic;

namespace PurpleGarlic
{
    public static class CSVTool
    {
        public static void LoadCsvTxt(string contentStr, out Dictionary<string, Dictionary<string, string>> big)
        {
            var lineAarry = contentStr.Split('\n');
            var row = lineAarry.Length - 1;
            var column = lineAarry[0].Split(',').Length;
            AnalysisCsvTxtStep(lineAarry, row, column, out big);
        }

        private static void AnalysisCsvTxtStep(string[] lineAarry, int row, int column,
            out Dictionary<string, Dictionary<string, string>> big)
        {
            var dic = new string[row, column];
            for (var i = 0; i < row; i++)
            for (var j = 0; j < column; j++)
                dic[i, j] = lineAarry[i].Split(',')[j].Trim();
            ChangeToDic(dic, out big);
        }

        private static void ChangeToDic(string[,] dic, out Dictionary<string, Dictionary<string, string>> big)
        {
            big = new Dictionary<string, Dictionary<string, string>>();
            var row = dic.GetLength(0);
            var col = dic.GetLength(1);
            for (var i = 0; i < row; i++)
            for (var j = 0; j < col; j++)
                if (i == 0)
                {
                    var small = new Dictionary<string, string>();
                    big.Add(dic[i, j], small);
                }

            foreach (var headfield in big.Keys)
            {
                if (headfield == "ID") continue;
                for (var i = 1; i <= row - 3; i++)
                    big[headfield].Add(i.ToString(), GetAnalysisCsvTxtWord(dic, headfield, i));
            }
        }

        //根据头字段和Id获取内容
        private static string GetAnalysisCsvTxtWord(string[,] dic, string headFieldName, int id)
        {
            //获取字段所在的行数
            var row = 0;
            var col = 0;
            for (var j = 0; j < dic.GetLength(1); j++)
                if (dic[0, j] == headFieldName)
                {
                    col = j;
                    break;
                }

            //获取id所在的列数
            for (var i = 0; i < dic.GetLength(0); i++)
                if (dic[i, 0] == id.ToString())
                {
                    row = i;
                    break;
                }

            var word = dic[row, col];
            return word == "" ? dic[2, col] : word;
        }
    }
}