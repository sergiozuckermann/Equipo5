const express = require("express");
const mysql = require("mysql2/promise");
const cors = require('cors');



const app = express();
app.use(cors());
const port = 8000;

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
        res.json(rows);

        // Close the database connection
        await connection.end();
    } catch (error) {
        console.error(error);
        res.status(500).json({ error: "Internal server error" });
    }
});


// Login to Database
app.post("/api/login", async (req, res) => {
    try {

        //Create a connection to the MySQL database
        const connection = await connectDB();
        // Execute a SELECT query to retrieve all users
        const [rows] = await connection.query("SELECT * FROM users WHERE username = ? AND password = ?", [req.body.username, req.body.password]);
        if (rows.length == 0) {
            res.status(401).json({ error: "Invalid username or password" });
            return;
        }
        console.log("Login Executed succesfully")
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
        res.json(rows[0]);
    } catch (error) {
        console.error(error);
        res.status(500).json({ error: "Internal server error" });
    }
});

app.post("/api/new_game", async (req, res) => {
    try {
        //Create a connection to the MySQL database
        const connection = await connectDB();
        // Execute a SELECT query to retrieve all users CREATE IN
        const [insert_data] = await connection.query("INSERT INTO game_sessions (user_id, time_on_seconds, number_of_battles, number_of_damaged_made, elements_obtained, finished) VALUES (?, ?, ?, ?, ?, ?)", [req.body.user_id, 0, 0, 0, 0, 0]);
        console.log("New game created succesfully")

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





app.listen(port, () => {
    console.log(`Server running at http://localhost:${port}`);
});