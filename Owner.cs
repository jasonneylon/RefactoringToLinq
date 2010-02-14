
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace RefactoringToLinq
{
public class Owner
    {
        public Owner(string name, params Whiskey[] whiskies)
        {
            Name = name;
            Whiskies = whiskies;
        }
        public string Name {get;set; }
        public IEnumerable<Whiskey> Whiskies { get; set; }
    }
}
