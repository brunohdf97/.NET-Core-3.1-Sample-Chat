var vueapp;
document.addEventListener("DOMContentLoaded", function () {

    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/chatHub")
        .build();


    vueapp = new Vue({
        el: '#mainapp',
        data: {
            connected_users: [],
            create_room: false,
            text_button: "Criar sala",
            title_li: "Clique para conversar",
            my_user_id: 0,

            text_chat: "",

            users_id_chat: [],
        },
        methods: {

            getTo: function (users_id, from) {

                var new_users_id = [];
                if (users_id.length > 0) {

                    users_id.forEach(el => {

                        if (el != from)
                            new_users_id.push(el);

                    });

                }

                return new_users_id;
            },

            getUser: function (user_id) {

                var user = null;
                this.connected_users.forEach(el => {

                    if (el.user.id == user_id) {
                        user = el.user;
                        return user;
                    }

                });

                return user;

            },

            getTextConversationUser: function (user_ids) {

                var self = this;
                var text = "";
                try {

                    if (user_ids.length > 0) {

                        for (var i in user_ids) {

                            var user = self.getUser(user_ids[i]);
                            if (user != null) {

                                text += user.name;
                                if (!((user_ids.length - 1) == i))
                                    text += user_ids.length == 2 ? " e " : ", ";
                            }
                        }
                    }
                }
                catch (e) {

                }

                return text.length > 0 ? "Conversa com " + text : "Conversa";
            },

            createAllChatsFromUserConnected: function () {

                var self = this;

                self.connected_users.forEach(el => {

                    if (el.user.id != self.my_user_id) {

                        var sustain_chats = document.getElementById("sustain-chats");

                        var chat_container_id = self.my_user_id + "_" + el.user.id;

                        var chat_container = sustain_chats.querySelector("#chat_" + chat_container_id);
                        if (chat_container == undefined || chat_container == null) {

                            chat_container = document.createElement("ul");
                            chat_container.id = "chat_" + chat_container_id;
                            chat_container.className = "chat";

                            sustain_chats.appendChild(chat_container);

                            var sustain_message = document.getElementById("sustain-message");
                            sustain_message.style.display = "none";

                            //hide all chats
                            var chats = sustain_chats.querySelectorAll(".chat");
                            chats.forEach(el => {

                                el.style.display = "none";

                            });

                        }


                    }

                });

                return false;

            },

            createPersonalChat: function (user_id) {

                var self = this;
                self.users_id_chat = [];

                if (!self.create_room && self.my_user_id == user_id)
                    alert("Você não pode conversar consigo mesmo...");
                else if (!self.create_room) {

                    self.text_chat = self.getTextConversationUser([self.my_user_id, user_id]);

                    var sustain_chats = document.getElementById("sustain-chats");

                    var sustain_message = document.getElementById("sustain-message");
                    sustain_message.style.display = "block";

                    //hide all chats
                    var chats = sustain_chats.querySelectorAll(".chat");
                    chats.forEach(el => {

                        el.style.display = "none";

                    });

                    var chat_container_id = self.my_user_id + "_" + user_id;
                    console.log('chat_container_id', chat_container_id);

                    var chat_container = sustain_chats.querySelector("#chat_" + chat_container_id);
                    if (chat_container == undefined || chat_container == null) {

                        chat_container = document.createElement("ul");
                        chat_container.id = "chat_" + chat_container_id;
                        chat_container.className = "chat";

                        sustain_chats.appendChild(chat_container);

                    }
                    else {

                        chat_container.style.display = "block";

                        var chat_select_chat = document.querySelector("#select_chat_" + chat_container_id);
                        var classname = chat_select_chat.className.replace('animation-highlight');
                        chat_select_chat.className = classname;
                    }

                    self.users_id_chat.push(user_id);

                    return false;
                }

                return false;
            },

            createRoomChat: function () {

                let create_room = document.querySelectorAll(".chk_create_room");
                let ids = [];
                create_room.forEach(el => {

                    if (el.checked) {
                        ids.push(el.value);
                    }
                });

                console.log('ids: ', ids);

                if (ids == undefined || ids == null || ids.length == 0) {
                    alert("Nenhum usuário foi selecionado");
                    return false;
                }
                else {


                    return true;
                }

                console.log('ids: ', ids);

            },

            showChat: function (from, to) {

                var self = this;
                if (from != self.my_user_id) {


                    var chat_container_id = from == self.my_user_id
                        ? from + "_" + to
                        : to == self.my_user_id
                            ? to + "_" + from
                            : "";

                    var chat_container = document.querySelector("#chat_" + chat_container_id);
                    if (chat_container.style.display != "block") {

                        var chat_select_chat = document.querySelector("#select_chat_" + chat_container_id);
                        if (chat_select_chat != undefined && chat_container_id != null) {

                            var classname = chat_select_chat.className.replace('animation-highlight');
                            chat_select_chat.className = classname + " animation-highlight";
                        }
                    }
                }
            },

            createMessage: function (from, to, message) {

                var self = this;

                var now = new Date().toLocaleDateString();

                var chat_container_id = from == self.my_user_id
                    ? from + "_" + to
                    : to == self.my_user_id
                        ? to + "_" + from
                        : "";

                var chat_container = document.querySelector("#chat_" + chat_container_id);

                var user_from = self.getUser(from);
                console.log('user_from: ', user_from);

                var el_li = document.createElement("li");
                el_li.innerHTML += "<p><b>" + user_from.name + ": </b>" + message +"</p> <p> <small>("+now+")</small></p>";
                chat_container.appendChild(el_li);

            },

            setCreateRoom: function () {

                this.create_room = !this.create_room;
                if (!this.create_room) {
                    this.text_button = "Criar sala";
                    this.createRoomChat();
                }
                else {
                    this.text_button = "Confirmar";
                    let create_room = document.querySelectorAll(".chk_create_room");
                    create_room.forEach(el => {

                        el.checked = false;

                    });
                }
            },

            sendMessage: function () {

                var self = this;
                if (self.users_id_chat.length > 0) {

                    var from = self.my_user_id;
                    var to = self.getTo(self.users_id_chat, from).join(',');

                    var sustain_message = document.getElementById("message");
                    var message = sustain_message.value;
                    sustain_message.value = "";
                    sustain_message.focus();

                    (async () => {
                        try {
                            await connection.invoke("SendChatMessage", from, to, message);
                        }
                        catch (e) {
                            console.error(e.toString());
                        }
                    })();

                }

            },

        },
        mounted: function () {

            var self = this;

            let my_user_id = document.getElementById("my_user_id");
            self.my_user_id = my_user_id.value;
            console.log('self.my_user_id', self.my_user_id);


            connection.on("ReceiveAllConnectedUsers", (connected_users) => {

                self.connected_users = connected_users;
                self.createAllChatsFromUserConnected();

            });

            connection.on("ReceiveMessage", (from, to, message) => {


                self.showChat(from, to);

                self.createMessage(from, to, message);

            });

            connection.on("ConnectedUsersUpdated", () => {

                (async () => {
                    try {
                        await connection.invoke("GetAllConnectedUsers");
                    }
                    catch (e) {
                        console.error(e.toString());
                    }
                })();

            });

            (async () => {
                try {
                    await connection.start();
                }
                catch (e) {
                    console.error(e.toString());
                }
            })();

        },
    });

});