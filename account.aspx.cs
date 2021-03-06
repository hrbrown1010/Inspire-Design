﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Driver;
using database_class;
using board_class;
using product_class;
using user_class;
using elements_class;

public partial class account : System.Web.UI.Page
{
    database myDB;
    user curUser;
    IMongoCollection<user> userInfoColl;
    protected void Page_Load(object sender, EventArgs e)
    {
        myDB = (database)Session["myDB"];
        curUser = (user)Session["curUser"];
        userInfoColl = myDB.getUserInfoCollection();
        curUser = user.findUser(userInfoColl, curUser.Username);
        Session["curUser"] = curUser;
        usernameLbl.Text = curUser.Username;
        emailAddressLbl.Text = curUser.Email;
        phoneLbl.Text = curUser.Phone;
        designerLbl.Text = curUser.Designer;
        passwordFieldValidator.ControlToValidate = "passwordTxt";
        cnfrmPassFieldValidator.ControlToValidate = "cnfrmPassTxt";
        changeEmailFieldValidator.ControlToValidate = "emailAddressTxt";
        if (curUser.Account_Type == "client")
        {
            clientPnl.Visible = true;
        }
        else
        {
            designerPnl.Visible = true;
            user.loadClients(userInfoColl, clientList, curUser);
        }
    }
    protected void changeEmailBtn_Click(object sender, EventArgs e)
    {
        emailAddressLbl.Visible = false;
        emailAddressTxt.Visible = true;
        emailAddressTxt.Text = curUser.Email;
        changeEmailBtn.Visible = false;
        changeEmailNoBtn.Visible = true;
        changeEmailYesBtn.Visible = true;
    }

    protected void changeEmailNoBtn_Click(object sender, ImageClickEventArgs e)
    {
        emailAddressTxt.Visible = false;
        emailAddressLbl.Visible = true;
        changeEmailNoBtn.Visible = false;
        changeEmailYesBtn.Visible = false;
        changeEmailBtn.Visible = true;
    }
    protected void changeEmailYesBtn_Click(object sender, ImageClickEventArgs e)
    {
        passwordFieldValidator.ControlToValidate = null;
        cnfrmPassFieldValidator.ControlToValidate = null;
        myDB.updateUserEmailDoc(userInfoColl, curUser, emailAddressTxt.Text);
        changeEmailYesBtn.Visible = false;
        changeEmailNoBtn.Visible = false;
        emailAddressTxt.Visible = false;
        emailAddressLbl.Visible = true;
        changeEmailBtn.Visible = true;
        Response.Redirect(Request.RawUrl);
    }
    protected void changePhoneBtn_Click(object sender, EventArgs e)
    {
        phoneLbl.Visible = false;
        phoneTxt.Visible = true;
        phoneTxt.Text = curUser.Phone;
        changePhoneBtn.Visible = false;
        changePhoneNoBtn.Visible = true;
        changePhoneYesBtn.Visible = true;
    }
    protected void changePhoneNoBtn_Click(object sender, ImageClickEventArgs e)
    {
        phoneTxt.Visible = false;
        phoneLbl.Visible = true;
        changePhoneNoBtn.Visible = false;
        changePhoneYesBtn.Visible = false;
        changePhoneBtn.Visible = true;
    }
    protected void changePhoneYesBtn_Click(object sender, ImageClickEventArgs e)
    {
        myDB.updateUserPhoneDoc(userInfoColl, curUser, phoneTxt.Text);
        changePhoneYesBtn.Visible = false;
        changePhoneNoBtn.Visible = false;
        phoneTxt.Visible = false;
        changePhoneBtn.Visible = true;
        phoneLbl.Visible = true;
        Response.Redirect(Request.RawUrl);
    }
    protected void changePassBtn_Click(object sender, EventArgs e)
    {
        passwordLbl.Visible = false;
        changePassBtn.Visible = false;
        passwordTxt.Visible = true;
        changePassYesBtn.Visible = true;
        changePassNoBtn.Visible = true;
        cnfrmPassPnl.Visible = true;
    }
    protected void changePassNoBtn_Click(object sender, ImageClickEventArgs e)
    {
        cnfrmPassPnl.Visible = false;
        changePassNoBtn.Visible = false;
        changePassYesBtn.Visible = false;
        passwordTxt.Visible = false;
        passwordLbl.Visible = true;
        changePassBtn.Visible = true;
    }
    protected void changePassYesBtn_Click(object sender, ImageClickEventArgs e)
    {
        changeEmailFieldValidator.ControlToValidate = null;
        if (passwordTxt.Text == cnfrmPassTxt.Text)
        {
            myDB.updateUserPassDoc(userInfoColl, curUser, passwordTxt.Text);
            passwordTxt.Visible = false;
            cnfrmPassPnl.Visible = false;
            changePassYesBtn.Visible = false;
            changePassNoBtn.Visible = false;
            changePassBtn.Visible = true;
            passwordLbl.Visible = true;
        }
        else
        {
            passErrorLbl.Text = "Passwords do not match.";
        }
    }
    protected void addClientBtn_Click(object sender, EventArgs e)
    {
        addClientTxt.Text = "";
        addClientBtn.Visible = false;
        addClientTxt.Visible = true;
        addClientYesBtn.Visible = true;
        addClientNoBtn.Visible = true;
    }
    protected void addClientNoBtn_Click(object sender, ImageClickEventArgs e)
    {
        addClientTxt.Visible = false;
        addClientYesBtn.Visible = false;
        addClientNoBtn.Visible = false;
        addClientBtn.Visible = true;
        errorLbl.Text = "";
    }
    protected void addClientYesBtn_Click(object sender, ImageClickEventArgs e)
    {

        if (myDB.addClient(userInfoColl, curUser, addClientTxt.Text, errorLbl) == true)
        {
            addClientTxt.Visible = false;
            addClientYesBtn.Visible = false;
            addClientNoBtn.Visible = false;
            addClientBtn.Visible = true;
            Response.Redirect(Request.RawUrl);
        }
    }
}