const express = require("express");
const mysql = require("mysql2/promise");
const cors = require('cors');
const { stat } = require("fs");



const app = express();
app.use(cors());
const port = 3010;

// Middleware to parse JSON request bodies
app.use(express.json());

// Route for creating a new user
async function connectDB() {
    const connection = await mysql.createConnection({
        host: "localhost",
        user: "root",
        password: "fulito99",
        database: "zazzacrifice",
    });

    return connection;
}

async function check_if_user_exists(username) {
    // Create a connection to the MySQL database
    const connection = await connectDB();
    // Execute a SELECT query to retrieve all users
    const [rows] = await connection.query("SELECT * FROM users WHERE username = ?", [username]);
    // Close the database connection
    await connection.end();
    if (rows.length > 0)
        return true;
    else
        return false;

}

// Route to get all users
app.get("/api/users", async (req, res) => {
    try {

        // Create a connection to the MySQL database
        const connection = await connectDB();
        // Execute a SELECT query to retrieve all users


        const [rows] = await connection.query("SELECT * FROM users");

        // Send the users as a JSON response
        console.log("Users retrieved succesfully")
        await connection.end();
        res.json(rows);

        // Close the database connection

    } catch (error) {
        console.error(error);
        res.status(500).json({ error: "Internal server error" });
    }
});


// Login to Database
app.post("/api/login", async (req, res) => {
    try {
        console.log("Login Executed")

        //Create a connection to the MySQL database
        const connection = await connectDB();
        // Execute a SELECT query to retrieve all users
        const [rows] = await connection.query("SELECT * FROM users WHERE username = ? AND password = ?", [req.body.username, req.body.password]);
        if (rows.length == 0) {
            res.status(401).json({ error: "Invalid username or password" });
            return;
        }
        console.log("Login Executed succesfully")
        await connection.end();
        res.json(rows[0].user_id);
    } catch (error) {
        console.error(error);
        res.status(500).json({ error: "Internal server error" });
    }
});

// Route to create a new user
app.post("/api/new_user", async (req, res) => {
    try {

        //Create a connection to the MySQL database
        const connection = await connectDB();
        //Check if user exists
        if (await check_if_user_exists(req.body.username)) {
            res.status(401).json({ error: "User already exists" });
            return;
        }
        // Execute a SELECT query to retrieve all users
        const [rows] = await connection.query("INSERT INTO users (username, password) VALUES (?, ?)", [req.body.username, req.body.password]);
        console.log("New user created succesfully")
        await connection.end();
        res.json(rows);
    } catch (error) {
        console.error(error);
        res.status(500).json({ error: "Internal server error" });
    }

});


app.get("/api/game_sessions", async (req, res) => {
    try {
        //Create a connection to the MySQL database
        const connection = await connectDB();
        // Execute a SELECT query to retrieve all users
        const [rows] = await connection.query("SELECT * FROM sessions_summary WHERE user_id = ?", [req.query.user_id]);
        console.log("Game_sessions Executed succesfully")
        await connection.end();
        res.json(rows[0]);
    } catch (error) {
        console.error(error);
        res.status(500).json({ error: "Internal server error" });
    }
});


app.get("/api/class_election_stats", async (req, res) => {

    try {
        //Create a connection to the MySQL database
        const connection = await connectDB();
        // Execute a SELECT query to retrieve all users
        const [rows] = await connection.query("SELECT * FROM class_percentage");

        [labels, data] = arrange_election_data(rows)

        console.log("Class_election Executed succesfully")
        await connection.end();
        res.json({ "labels": labels, "data": data });
    }
    catch (error) {
        console.error(error);
        res.status(500).json({ error: "Internal server error" });
    }
});

function arrange_election_data(rows) {
    let labels = []
    let data = []
    for (let i = 0; i < rows.length; i++) {
        labels.push(rows[i].name)
        data.push(rows[i].percentage)
    }
    return [labels, data]
}
///////////////////////// API DAMAGE MADE_VS_RECEIVED  ///////////////////////////
app.get("/api/damage_made_vs_received", async (req, res) => {
    try {
        //Create a connection to the MySQL database
        const connection = await connectDB();
        // Execute a SELECT query to retrieve all users
        const [rows] = await connection.query("SELECT * FROM damage_made_vs_received");
        [damage_made, damage_received] = get_average_from_damage_in_batttle(rows)
        console.log("Damage_made_vs_received Executed succesfully")
        await connection.end();
        res.json({ "damage_made": damage_made, "damage_received": damage_received });
    }
    catch (error) {
        console.error(error);
        res.status(500).json({ error: "Internal server error" });
    }

});

function arrange_data_for_damage_made_vs_received_chart(rows) {
    player_ids = [];
    damage_made = [];
    damage_received = [];
    times_found = [];
    cont = 0
    for (let i = 0; i < rows.length; i++) {
        if (!player_ids.includes(rows[i]["player_id"])) {
            cont = 0
            player_ids.push(rows[i]["player_id"])
        }

        if (cont >= damage_made.length) {
            damage_made.push(rows[i]["total_damage_made"])
            damage_received.push(rows[i]["total_damage_received"])
            times_found.push(1)
        }
        else {
            damage_made[cont] += rows[i]["total_damage_made"]
            damage_received[cont] += rows[i]["total_damage_received"]
            times_found[cont] += 1
        }

        cont += 1
    }

    return [damage_made, damage_received, times_found]
}

function get_average_from_damage_in_batttle(rows) {
    [damage_made, damage_received, times_found] = arrange_data_for_damage_made_vs_received_chart(rows)

    for (let i = 0; i < damage_made.length; i++) {
        damage_made[i] = damage_made[i] / times_found[i]
        damage_received[i] = damage_received[i] / times_found[i]
    }
    return [damage_made, damage_received]
}

////////////// API ENEMY WIN RATE //////////////////////
app.get("/api/enemy_win_rate", async (req, res) => {
    try {
        //Create a connection to the MySQL database
        const connection = await connectDB();
        // Execute a SELECT query to retrieve all users
        const [rows] = await connection.query("SELECT * FROM enemy_win_rate");
        [labels, enemyloses, enemywins] = arrange_data_for_enemy_win_rate(rows)
        console.log("Enemy_win_rate Executed succesfully")
        await connection.end();
        res.json({ "labels": labels, "enemyloses": enemyloses, "enemywins": enemywins });
    }
    catch (error) {
        console.error(error);
        res.status(500).json({ error: "Internal server error" });
    }
});

function arrange_data_for_enemy_win_rate(rows) {
    let labels = []
    let enemywins = []
    let enemyloses = []
    let i = 0
    while (i < rows.length) {
        if (rows[i].enemy === rows[i + 1].enemy) {
            labels.push(rows[i].enemy)
            enemyloses.push(rows[i].count)
            enemywins.push(rows[i + 1].count)

            i += 2
        }
        else if (rows[i].battle_result.readUInt8() === 0) {
            labels.push(rows[i].enemy)
            enemyloses.push(rows[i].count)
            enemywins.push(0)
            i += 1
        }
        else {
            labels.push(rows[i].enemy)
            enemyloses.push(0)
            enemywins.push(rows[i].count)
            i += 1
        }
    }

    //result.push(rows[i].battle_result.readUInt8())
    return [labels, enemywins, enemyloses]
}


////////////// ATTACK USES API //////////////////////
app.get("/api/attack_uses", async (req, res) => {
    try {

        //Create a connection to the MySQL database
        const connection = await connectDB();
        // Execute a SELECT query to retrieve all users
        const [rows] = await connection.query("SELECT * FROM attack_uses");
        console.log("Attack_uses Executed succesfully")
        await connection.end();
        return res.json(rows);
    }
    catch (error) {
        console.error(error);
        res.status(500).json({ error: "Internal server error" });
    }
});

////////////// API BATTLE RESULTS //////////////////////
app.get("/api/criticals_vs_missed", async (req, res) => {
    try {
        //Create a connection to the MySQL database
        const connection = await connectDB();
        // Execute a SELECT query to retrieve all users
        const [rows] = await connection.query("SELECT * FROM criticals_vs_missed");
        console.log("Criticals_vs_missed Executed succesfully")
        return res.json(rows);
    }
    catch (error) {
        console.error(error);
        res.status(500).json({ error: "Internal server error" });
    }
});

////////////// API POST GAME_ //////////////////////
app.post("/api/new_game_session", async (req, res) => {
    try {
        console.log(req.body)

        //Create a connection to the MySQL database
        const connection = await connectDB();

        //INSERT GAME SESSION
        const [response_game_session] = (await connection.query("INSERT INTO game_sessions (user_id) VALUES (?)", [req.body.user_id]))
        const game_session_id = response_game_session.insertId
        console.log("Game_session Executed succesfully", game_session_id)
        // INSERT PLAYER 
        const [response_player] = await connection.query("INSERT INTO players (game_session_id, class_id) VALUES (?,?)", [game_session_id, req.body.class_id])
        const player_id = response_player.insertId
        console.log("Player Executed succesfully", player_id)
        // Parse Stats from body
        const stats = JSON.parse(req.body.stats)
        insertStats(connection, player_id, stats)

        await connection.end();
        return res.json({ "game_session_id": game_session_id, "player_id": player_id })

    }
    catch (error) {
        console.error(error);
        res.status(500).json({ error: "Internal server error" });
    }
});

async function insertStats(connection, player_id, stats) {
    // INSERT STATS

    const [response_stats] = await connection.query(`INSERT INTO stats_players (player_id, stat_id, value)\
                                                VALUES (${player_id}, 1, ${stats.defence}),\
                                                        (${player_id}, 2, ${stats.damage}),\
                                                        (${player_id}, 3, ${stats.agility}),\
                                                        (${player_id}, 4, ${stats.luck}),\
                                                        (${player_id}, 5, ${stats.charisma}),\
                                                        (${player_id}, 6, ${stats.accuracy}),\
                                                        (${player_id}, 7, ${stats.maxHP}),\
                                                        (${player_id}, 8, ${stats.currentHP}),\
                                                        (${player_id}, 9, ${stats.maxMP}),\
                                                        (${player_id}, 10, ${stats.currentMP})`)
    await connection.end();
}

////////////// UPDATE GAME SESSION //////////////////////
app.post("/api/update_game_session", async (req, res) => {
    try {
        console.log("Update Game_session", req.body)
        // Update Game Session
        player_info = JSON.parse(req.body.stats)
        const connection = await connectDB();
        const response_game_session = await connection.query("UPDATE game_sessions SET finished = ? WHERE game_session_id = ?", [req.body.is_finished, req.body.game_session_id])
        console.log("Game_session Updated succesfully")
        // Update Player
        const response_player = await connection.query("UPDATE players SET money = ?  WHERE player_id = ?", [player_info.coins, req.body.player_id])
        console.log("Player Updated succesfully")
        // Update Stats
        update_stats(connection, req.body.player_id, player_info)
        console.log("Stats Updated succesfully")
        // Update Attacks
        update_attacks(req.body.player_id, player_info)
        console.log("Attacks Updated succesfully")
        // Update Checkpoints 
        const checkpoint = await connection.query("UPDATE checkpoints SET scene_id = ?, x_position = ?, y_position = ? WHERE player_id = ?", [req.body.place, req.body.x, req.body.y, req.body.player_id])
        console.log("Checkpoint Updated succesfully")
        insert_battle_stats(req.body)
        console.log("Battle Stats Updated succesfully")
        await connection.end();

        return res.json({ "Updated": "Success" });
        //Update 




    }
    catch (error) {
        console.error(error);
        res.status(500).json({ error: "Internal server error" });
    }

});

async function update_stats(connection, player_id, stats) {
    // UPDATE STATS
    const response_stats = await connection.query(`
      UPDATE stats_players
      SET value = CASE stat_id
        WHEN 1 THEN ${stats.defence}
        WHEN 2 THEN ${stats.damage}
        WHEN 3 THEN ${stats.agility}
        WHEN 4 THEN ${stats.luck}
        WHEN 5 THEN ${stats.charisma}
        WHEN 6 THEN ${stats.accuracy}
        WHEN 7 THEN ${stats.maxHP}
        WHEN 8 THEN ${stats.currentHP}
        WHEN 9 THEN ${stats.maxMP}
        WHEN 10 THEN ${stats.currentMP}
      END
      WHERE player_id = ${player_id}
    `);

    return { "player_id": player_id, "updated_rows": response_stats.affectedRows };
}

async function update_attacks(player_id, stats) {
    // UPDATE ATTACKS
    const connection = await connectDB();

    if (stats.firea == true) {
        console.log("!firea")
        const [rows1] = await connection.query("SELECT * FROM players_attacks WHERE player_id = ? AND attack_id = ?", [player_id, 2])
        if (rows1.length == 0) {
            const [res1] = await connection.query("INSERT INTO players_attacks (player_id, attack_id) VALUES (?, ?)", [player_id, 2])
        }
    }

    if (stats.icea == true) {
        console.log("!icea")
        const [rows2] = await connection.query("SELECT * FROM players_attacks WHERE player_id = ? AND attack_id = ?", [player_id, 4])
        if (rows2.length == 0) {

            const [res2] = await connection.query("INSERT INTO players_attacks (player_id, attack_id) VALUES (?, ?)", [player_id, 4])
        }
    }

    if (stats.lightninga == true) {
        console.log("!lightninga")
        const [rows3] = await connection.query("SELECT * FROM players_attacks WHERE player_id = ? AND attack_id = ?", [player_id, 3])
        if (rows3.length == 0) {

            const [res3] = await connection.query("INSERT INTO players_attacks (player_id, attack_id) VALUES (?, ?)", [player_id, 3])
        }
    }

}

async function insert_battle_stats(obj)
{
    const connection = await connectDB();

    const damage_made = obj.damagemade.split("|")
    let damage_made_num = damage_made.map(Number);
    damage_made_num = damage_made_num.slice(1);

    const damage_received = obj.damagereceived.split("|")
    let damage_received_num = damage_received.map(Number);
    damage_received_num = damage_received_num.slice(1);

    const coin_received = obj.coinsreceived.split("|")
    let coin_received_num = coin_received.map(Number);
    coin_received_num = coin_received_num.slice(1);


    const misses = obj.misses.split("|")
    let misses_num = misses.map(Number);
    misses_num = misses_num.slice(1);

    const crits = obj.crits.split("|")
    let crits_num = crits.map(Number);
    crits_num = crits_num.slice(1);

    const melees = obj.melees.split("|")
    let melees_num = melees.map(Number);
    melees_num = melees_num.slice(1);
    console.log(melees_num)

    const iceuses = obj.iceuses.split("|")
    let iceuses_num = iceuses.map(Number);
    iceuses_num = iceuses_num.slice(1);

    const fireuses = obj.fireuses.split("|")
    let fireuses_num = fireuses.map(Number);
    fireuses_num = fireuses_num.slice(1);

    const thunderuses = obj.thunderuses.split("|")
    let thunderuses_num = thunderuses.map(Number);
    thunderuses_num = thunderuses_num.slice(1);

    const healuses = obj.healuses.split("|")
    let healuses_num = healuses.map(Number);
    healuses_num = healuses_num.slice(1);

    const rechargeuses = obj.rechargeuses.split("|")
    let rechargeuses_num = rechargeuses.map(Number);
    rechargeuses_num = rechargeuses_num.slice(1);

    const results = obj.results.split("|")
    let results_num = results.map(Number);
    results_num = results_num.slice(1);

    for (let i = 0; i < damage_made_num.length; i++) {
        const [battle_id] = await connection.query("INSERT INTO battles (player_id, enemy, total_damage_made, total_damage_received, coin_received, battle_result, attacks_missed, critical_attacks) VALUES (?, ?, ?, ?, ?, ?, ?, ?)", [obj.player_id, "ZAZZA", damage_made_num[i], damage_received_num[i], coin_received_num[i], results_num[i], misses_num[i], crits_num[i]])

        values = [
            battle_id.insertId, 1, melees_num[i],
            battle_id.insertId, 2, iceuses_num[i],
            battle_id.insertId, 3, thunderuses_num[i],
            battle_id.insertId, 4, fireuses_num[i],
            battle_id.insertId, 5, healuses_num[i],
            battle_id.insertId, 6, rechargeuses_num[i] 
        ]
        const [res] = await connection.query("INSERT INTO battles_attacks (battle_id, attack_id, times_used) VALUES (?, ?, ?), (?, ?, ?), (?, ?, ?), (?, ?, ?), (?, ?, ?), (?, ?, ?)", values)

    }
    connection.end();


}


////////////// GET GAME SESSION //////////////////////
app.get("/api/get_game_session", async (req, res) => {

    try {
        const connection = await connectDB();
        const [rows] = await connection.query("SELECT player_id, game_session_id FROM players INNER JOIN game_sessions using (game_session_id) INNER JOIN users using (user_id) WHERE user_id = ? order by player_id desc LIMIT 1", [req.query.user_id])
        finished = await get_if_finished(connection, rows[0].game_session_id)
        const [stats] = await connection.query("SELECT value FROM stats_players WHERE player_id = ? order by stat_id;", [rows[0].player_id])
        const [coins] = await connection.query("SELECT money FROM players WHERE player_id = ?", [rows[0].player_id])
        const [attacks] = await connection.query("SELECT attack_id FROM players_attacks WHERE player_id = ?", [rows[0].player_id])
        const [checkpoint] = await connection.query("SELECT scene_id as place, x_position, y_position FROM checkpoints WHERE player_id = ?", [rows[0].player_id])
        await connection.end();
        const shaggy = make_shaggy_json(coins, stats, attacks, checkpoint[0].place)
        final_json = { "shaggy": JSON.stringify(shaggy), "finished": finished, "x": checkpoint[0].x_position, "y": checkpoint[0].y_position, "game_session_id": rows[0].game_session_id, "player_id": rows[0].player_id }
        return res.json(final_json);
    }
    catch (error) {
        console.error(error);
        res.status(500).json({ error: "Internal server error" });
    }
});


async function get_if_finished(connection, game_session_id) {
    const [rows] = await connection.query("SELECT finished FROM game_sessions WHERE game_session_id = ?", [game_session_id])
    const finished = rows[0].finished
    const intf = finished.readIntBE(0, finished.length);
    return intf
}


function make_shaggy_json(coins, stats, attacks, place) {
    let shaggy = { "dead": 0, "fire": 0, "ice": 0, "lightning": 0, "index": 0, "number": 0}

    shaggy["defence"] = stats[0].value
    shaggy["damage"] = stats[1].value
    shaggy["agility"] = stats[2].value
    shaggy["luck"] = stats[3].value
    shaggy["charisma"] = stats[4].value
    shaggy["accuracy"] = stats[5].value
    shaggy["maxHP"] = stats[6].value
    shaggy["currentHP"] = stats[7].value
    shaggy["maxMP"] = stats[8].value
    shaggy["currentMP"] = stats[9].value


    shaggy["coins"] = coins[0].money

    shaggy["place"] = place - 1
    shaggy["firea"] = false
    shaggy["lightninga"] = false
    shaggy["icea"] = false

    for (let i = 0; i < attacks.length; i++) {
        if (attacks[i].attack_id == 2) {
            shaggy["firea"] = true
        }
        if (attacks[i].attack_id == 3) {
            shaggy["lightninga"] = true
        }
        if (attacks[i].attack_id == 4) {
            shaggy["icea"] = true
        }
    }
    return shaggy
}

app.get("/api/get_game_session_id", async (req, res) => {
    console.log("Get Game_session_id", req.query)
    try {
        const connection = await connectDB();
        const [rows] = await connection.query("SELECT g.game_session_id, p.player_id FROM game_sessions as g INNER JOIN \
        players as p using(game_session_id) WHERE user_id = ?", [req.query.user_id])
        await connection.end();
        const data = arrange_ids(rows)
        return res.json(data);

    }
    catch (error) {
        console.error(error);
        res.status(500).json({ error: "Internal server error" });
    }
});

function arrange_ids(rows) {
    let player_ids = []
    let game_session_id = []
    for (let i = 0; i < rows.length; i++) {
        player_ids.push(rows[i].player_id)
        game_session_id.push(rows[i].game_session_id)
    }
    return { "player_ids": player_ids, "game_session_id": game_session_id }

}

app.listen(port, () => {
    console.log(`Server running at http://localhost:${port}`);
});
