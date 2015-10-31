﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Bson;
using MongoDB.Driver;
using database_class;
using board_class;
using user_class;
using product_class;

public partial class inspiration : System.Web.UI.Page
{
    database myDB;
    string boardName;
    user curUser;
    IMongoCollection<board_item> usersBoardColl;
    protected void Page_Load(object sender, EventArgs e)
    {
        myDB = (database)Session["myDB"];
        boardName = (string)Session["boardName"];
        curUser = (user)Session["curUser"];
        usersBoardColl = myDB.getUsersBoardCollection(curUser);
    }
}