/* https://www.oktatas.hu/kozneveles/erettsegi/feladatsorok/emelt_szint_2017tavasz/emelt_9nap
00:01 - 00:03
So phase two - tank creation.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

// 2. Hozzon létre saját osztályt IdozitettFelirat azonosítóval és definiáljon benne két szöveg típusú adattagot, 
//    melyben egy felirat időzítését és magát a feliratot tudja majd tárolni
class IdozitettFelirat{

    public int start_ora     {get; set;}
    public int start_perc    {get; set;}
    public int start_sec     {get; set;}
    public int stop_ora      {get; set;}
    public int stop_perc     {get; set;}
    public int stop_sec      {get; set;}
    
    public string idozites  {get; set;}
    public string srt_idozites {get; set;}
    public string felirat   {get; set;}
    public int  SzavakSzama {get; set;}
    /*
    3. Készítse el az osztály konstruktorát, ami a következő feladatokat hajtja végre!
        a. Beállítja az időzítést tároló adattag értékét a konstruktor paraméterében megadott értékkel.
        b. Beállítja a felirat szövegét tároló adattag értékét a konstruktor paraméterében megadott értékkel.
    */
    public IdozitettFelirat(string sor)
    {   var s = sor.Split(";");
        idozites = s[0];
        felirat  = s[1];
        start_ora  = int.Parse( idozites.Substring(0,2) ) / 60;
        start_perc = int.Parse( idozites.Substring(0,2) ) % 60;
        start_sec  = int.Parse( idozites.Substring(3,2) );
        stop_ora  = int.Parse( idozites.Substring(8,2) ) / 60;
        stop_perc = int.Parse( idozites.Substring(8,2) ) % 60;
        stop_sec  = int.Parse( idozites.Substring(11,2) );
        
    // 6. Készítsen az IdozitettFelirat osztályban jellemzőt vagy metódust SzavakSzama azonosítóval! 
    //    A létrehozott jellemző vagy metódus segítségével határozza meg az időzített felirat szavainak a számát!         
        SzavakSzama = felirat.Split().Count();

    // 8. Készítsen az IdozitettFelirat osztályban jellemzőt vagy metódust SrtIdozites azonosítóval!
        srt_idozites = $"{start_ora:00}:{start_perc:00}:{start_sec:00} --> {stop_ora:00}:{stop_perc:00}:{stop_sec:00}";
    }
}

class Program 
{
    public static void Main (string[] args) 
    {
    
    // 4. Olvassa be a feliratok.txt állomány sorait és hozzon létre osztálypéldányt (objektumot) minden egyes időzítés−felirat párhoz! 
    //Az osztálypéldányokat egy összetett változóban (pl. vektor, lista stb.) tárolja!

        var sr      = new StreamReader("feliratok.txt");
        var lista   = new List<IdozitettFelirat>();
        
        while(!sr.EndOfStream)
        {
            var sor = sr.ReadLine() + ";" + sr.ReadLine();
            //Console.WriteLine(sor);
            lista.Add(new IdozitettFelirat(sor));
        }
        sr.Close();

    //5. Határozza meg és írja ki a képernyőre a minta szerint, hogy hány felirat van a feliratok.txt állományban!
        Console.WriteLine($"5. feladat - Feliratok száma: {lista.Count}");

        var legtobb_szo = 
        (
            from sor in lista
            orderby sor.SzavakSzama
            select sor.felirat
        ).Last();

    // 7. Határozza meg és írja ki a legtöbb szóból álló feliratot! Feltételezheti, hogy a feliratfájlban csak egy ilyen felirat van. 
    //    Az eredményt a minta szerint jelenítse meg a képernyőn!
        Console.WriteLine($"7. feladat - Legtöbb szóból álló felirat:");
        Console.WriteLine($"{legtobb_szo}");

    /*
       9. Készítse el a felirat.srt állományt a minta szerint! 
            Az állományba kerüljön bele a felirat száma (a számozás 1-től kezdődik), 
            az SRT időzítése és a felirat szövege! 
            A feliratokat egy-egy üres sor válassza el egymástól!
    */
        var sw = new StreamWriter("felirat.srt");
        int szamlalo = 1;
        foreach(var sor in lista)
        {
            sw.WriteLine(szamlalo);
            sw.WriteLine(sor.srt_idozites);
            sw.WriteLine(sor.felirat);
            sw.WriteLine();
            szamlalo++;
        }
        sw.Close();
    }
}