
//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace DataAccess
{

using System;
    using System.Collections.Generic;
    
public partial class PatientsDieseases
{

    public PatientsDieseases()
    {

        this.Examinations = new HashSet<Examinations>();

    }


    public int Id { get; set; }

    public int PatientId { get; set; }

    public int DieseaseId { get; set; }

    public string Description { get; set; }



    public virtual Dieseases Dieseases { get; set; }

    public virtual Patients Patients { get; set; }

    public virtual ICollection<Examinations> Examinations { get; set; }

}

}
