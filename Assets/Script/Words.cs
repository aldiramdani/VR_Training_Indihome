using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class Words : IEquatable<Words>
{
    // Start is called before the first frame update
    public string kataKunci{get;set;}
    public string isWajib{get;set;}
    public string kataWajib{get;set;}
    public double nilai{get;set;}
    public string skenarioTujuan{get;set;}
    public string toDo{get;set;}

    public bool Equals(Words other){
        if(other == null) return false;
        return(this.skenarioTujuan.Equals(other.skenarioTujuan));
    }
}
