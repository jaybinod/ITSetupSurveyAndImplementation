using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Romasoft
/// </summary>
public class Romasoft
{
	public Romasoft()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string IncreaseNext(string mstr)
    {

        string str = mstr;
        if (str.Trim().Length == 0)
            str = "1";
        else
        {
            bool act = false;
            char[] l1;
            l1 = str.Trim().ToUpper().ToCharArray(0, str.Trim().Length);
            int i = l1.Length - 1;

            for (; i >= 0; i--)
            {
                if (int.Parse(Convert.ToInt16(l1[i]).ToString()) != 57 && int.Parse(Convert.ToInt16(l1[i]).ToString()) != 90)
                {
                    int nw = int.Parse(Convert.ToInt16(l1[i]).ToString()) + 1;
                    l1[i] = Convert.ToChar(nw);
                    int j = i + 1;
                    for (; j < l1.Length; j++)
                    {
                        if (int.Parse(Convert.ToInt16(l1[j]).ToString()) == 57)
                        {
                            l1[j] = Convert.ToChar(48);
                        }
                        if (int.Parse(Convert.ToInt16(l1[j]).ToString()) == 90)
                        {
                            l1[j] = Convert.ToChar(65);
                        }
                    }
                    act = true;
                    break;
                }
            }
            string nc = "";

            if (act == false)
            {
                char n = l1[0];

                if (int.Parse(Convert.ToInt16(n).ToString()) == 57)
                {
                    nc = "1";
                }
                if (int.Parse(Convert.ToInt16(n).ToString()) == 90)
                {
                    nc = "A";
                }
            }

            str = string.Empty;
            i = l1.Length - 1;
            if (act == false)
                for (; i >= 0; i--)
                {
                    int j = 0;
                    for (; j < l1.Length; j++)
                    {
                        if (int.Parse(Convert.ToInt16(l1[j]).ToString()) == 57)
                        {
                            l1[j] = Convert.ToChar(48);
                        }
                        if (int.Parse(Convert.ToInt16(l1[j]).ToString()) == 90)
                        {
                            l1[j] = Convert.ToChar(65);
                        }
                    }
                    break;
                }

            int ln = l1.Length - 1;
            for (; ln >= 0; ln--)
                str = l1[ln] + str;

            if (act == false)
                str = nc + str;
        }
        return str;
    }


}