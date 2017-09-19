/*
 * SMART CHAT ENGINE (EXTENTION)
 * Copyright (c) 2013 Wen Pu
 * Modified by MyOrange
 * All modifications made are hereby copyright (c) 2014-2015 MyOrange
 */

// clears the variable if left blank
// Need this to make IE happy
// see http://soledadpenades.com/2007/05/17/arrayindexof-in-internet-explorer/
/*if (!Array.indexOf) {
    Array.prototype.indexOf = function (obj) {
        for (var i = 0; i < this.length; i++) {
            if (this[i] == obj) {
                return i;
            }
        }
        return -1;
    }
}*/

chathub = $.connection.signalHub;


var chatboxManager = function () {
		
    var init = function (options) {
        $.extend(chatbox_config, options)
    };


    var delBox = function (id) {
        // TODO
    };

    var getNextOffset = function () {
        return (chatbox_config.width + chatbox_config.gap) * showList.length;
    };

    var boxClosedCallback = function (id) {
        // close button in the titlebar is clicked
        var idx = showList.indexOf(id);
        if (idx != -1) {
            showList.splice(idx, 1);
            console.log("da xoa"+ id);
            diff = chatbox_config.width + chatbox_config.gap;
            for (var i = idx; i < showList.length; i++) {
                offset = $("#" + showList[i]).chatbox("option", "offset");
                $("#" + showList[i]).chatbox("option", "offset", offset - diff);
            }
        } else {
            alert("NOTE: Id missing from array: " + id);
        }
    };

    // caller should guarantee the uniqueness of id
    var addBox = function (id, user, name) {
        var idx1 = showList.indexOf(id);
        var idx2 = boxList.indexOf(id);
        if (idx1 != -1) {
            // found one in show box, do nothing
            console.log("addbox da show" + id);
        } else if (idx2 != -1) {
            // exists, but hidden
            console.log("addbox da co nhung hiden" + id);
            // show it and put it back to showList
            $("#" + id).chatbox("option", "offset", getNextOffset());
            var manager = $("#" + id).chatbox("option", "boxManager");
            manager.toggleBox();
            showList.push(id);
        } else {
            var el = document.createElement('div');
            el.setAttribute('id', id);
            $(el).chatbox({
                id: id,
                user: user,
                title: '<i title="' + user.status + '"></i>' + user.first_name + " " + user.last_name,
                hidden: false,
                offset: getNextOffset(),
                width: chatbox_config.width,
                status: user.status,
                alertmsg: user.alertmsg,
                alertshow: user.alertshow,
                messageSent: dispatch,
                boxClosed: boxClosedCallback
            });
            boxList.push(id);
            showList.push(id);
            nameList.push(user.first_name);
        }
    };

    var messageSentCallback = function (id, user, msg) {
        var idx = boxList.indexOf(id);
        var idx2 = showList.indexOf(id);
        if (idx != -1) {
            //da co
            console.log("da co");
            if (idx2 != -1) {
                //da show
                console.log("da show");
                $("#" + id).chatbox("option", "boxManager").addMsg(user, msg);
            } else {
                //chua show
                console.log("chua show");
                $("#" + id).chatbox("option", "offset", getNextOffset());
                var manager = $("#" + id).chatbox("option", "boxManager");
                $("#" + id).chatbox("option", "boxManager").addMsg(user, msg);
                showList.push(id);
            }
        } else {
            //chua co
            NewChatBox(id, user);
            $("#" + id).chatbox("option", "boxManager").addMsg(user, msg);
        }
    };


    // not used in demo
    var dispatch = function (id, user, msg) {
        //$("#log").append("<i>" + moment().calendar() + "</i> you said to <b>" + user.first_name + " " + user.last_name + ":</b> " + msg + "<br/>");
        //if ($('#chatlog').doesExist()){
        //	$("#chatlog").append("You said to <b>" + user.first_name + " " + user.last_name + ":</b> " + msg + "<br/>").effect("highlight", {}, 500);;
        //}
        $("#" + id).chatbox("option", "boxManager").addMsg("Me", msg);
        chathub.server.sendPrivate(id, msg);
    }

    return {
        init: init,
        addBox: addBox,
        delBox: delBox,
        dispatch: dispatch,
        messageSentCallback: messageSentCallback,
    };
}();


function NewChatBox(id, fname, lname, status, alertmsg, alertshow) {
    var $this = $(this),
        temp_chat_id = id,
        fname = fname || "",
        lname = lname || "",
        status = status || "online",
        alertmsg = alertmsg || "",
        alertshow = alertshow || false;


    chatboxManager.addBox(temp_chat_id, {
        // dest:"dest" + counter, 
        // not used in demo
        title: "username" + temp_chat_id,
        first_name: fname,
        last_name: lname,
        status: status,
        alertmsg: alertmsg,
        alertshow: alertshow
        //you can add your own options too
    });
    event.preventDefault();
};



