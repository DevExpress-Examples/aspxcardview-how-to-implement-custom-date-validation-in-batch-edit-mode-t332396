﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class _Default : System.Web.UI.Page
{
    private bool CheckYears(DateTime hireDate, DateTime birthDate)
    {
        TimeSpan span = hireDate - birthDate;
        if (span.TotalDays / 365 < 18)
            return false;
        else
            return true;
    }
    void AddError(Dictionary<CardViewColumn, string> errors, CardViewColumn column, string errorText)
    {
        if (errors.ContainsKey(column)) return;
        errors[column] = errorText;
    }

    protected void ASPxCheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        ASPxCardView1.SettingsEditing.BatchEditSettings.AllowValidationOnEndEdit = !ASPxCheckBox2.Checked;
    }
    protected void ASPxCardView1_CardValidating(object sender, ASPxCardViewDataValidationEventArgs e)
    {
        DateTime birthDate = (DateTime)e.NewValues["BirthDate"];
        DateTime hireDate = (DateTime)e.NewValues["HireDate"];
        var result = CheckYears(hireDate, birthDate);
        if (!result)
        {
            AddError(e.Errors, ASPxCardView1.Columns["BirthDate"], "Invalid difference between Hire Date and Birth Date");
            AddError(e.Errors, ASPxCardView1.Columns["HireDate"], "Invalid difference between Hire Date and Birth Date");
            e.CardError = "Correct validation errors";
        }
        else
        {
            e.CardError = "Modifications aren't allowed in the online example. </br>If you need to test the data editing functionality, please download the example on your machine and run it locally.";
        }
    }

}