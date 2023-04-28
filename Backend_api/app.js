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
        password: "Zazza123",
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
        console.log(req.body);
        console.log("api accedido");
        // Create a connection to the MySQL database
        const connection = await connectDB();
        // Execute a SELECT query to retrieve all users


        const [rows] = await connection.query("SELECT * FROM users");

        // Send the users as a JSON response
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
        console.log("Hola")
        console.log(req.body);
        //Create a connection to the MySQL database
        const connection = await connectDB();
        // Execute a SELECT query to retrieve all users
        const [rows] = await connection.query("SELECT * FROM users WHERE username = ? AND password = ?", [req.body.username, req.body.password]);
        if (rows.length == 0) {
            res.status(401).json({ error: "Invalid username or password" });
            return;
        }
        res.json(rows[0].user_id);
    } catch (error) {
        console.error(error);
        res.status(500).json({ error: "Internal server error" });
    }
});

// Route to create a new user
app.post("/api/new_user", async (req, res) => {
    try {
        console.log(req.body);

        //Create a connection to the MySQL database
        const connection = await connectDB();
        //Check if user exists
        if (await check_if_user_exists(req.body.username)) {
            res.status(401).json({ error: "User already exists" });
            return;
        }
        // Execute a SELECT query to retrieve all users
        const [rows] = await connection.query("INSERT INTO users (username, password) VALUES (?, ?)", [req.body.username, req.body.password]);
        res.json(rows);
    } catch (error) {
        console.error(error);
        res.status(500).json({ error: "Internal server error" });
    }

});


app.get("/api/game_sessions", async (req, res) => {
    try {
        //Create a connection to the MySQL database
        console.log("REq")
        const connection = await connectDB();
        // Execute a SELECT query to retrieve all users
        const [rows] = await connection.query("SELECT * FROM sessions_summary WHERE user_id = ?", [req.query.user_id]);
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
        console.log(req.body);
        const [insert_data] = await connection.query("INSERT INTO game_sessions (user_id, time_on_seconds, number_of_battles, number_of_damaged_made, elements_obtained, finished) VALUES (?, ?, ?, ?, ?, ?)", [req.body.user_id, 0, 0, 0, 0, 0]);

    } catch (error) {
        console.error(error);
        res.status(500).json({ error: "Internal server error" });
    }
});

app.get("/api/class_election_stats", async (req, res) => {
    console.log("Class_election_Api Call")
    try {
        //Create a connection to the MySQL database
        const connection = await connectDB();
        // Execute a SELECT query to retrieve all users
        const [rows] = await connection.query("SELECT * FROM class_percentage");

        [labels, data] = arrange_election_data(rows)
        console.log(labels)
        console.log(data)

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
    console.log("Damage_made_vs_received_Api Call")
    try {
        //Create a connection to the MySQL database
        const connection = await connectDB();
        // Execute a SELECT query to retrieve all users
        const [rows] = await connection.query("SELECT * FROM damage_made_vs_received");
        [damage_made, damage_received] = get_average_from_damage_in_batttle(rows)

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
    console.log(rows)
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
    console.log([damage_made, damage_received, times_found])
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
    console.log("Enemy_win_rate_obtained_Api Call")
    try {
        //Create a connection to the MySQL database
        const connection = await connectDB();
        // Execute a SELECT query to retrieve all users
        const [rows] = await connection.query("SELECT * FROM enemy_win_rate");
        [labels, count, result] = arrange_data_for_enemy_win_rate(rows)
        return res.json({ "labels": labels, "count": count, "result": result });
    }
    catch (error) {
        console.error(error);
        res.status(500).json({ error: "Internal server error" });
    }
});

function arrange_data_for_enemy_win_rate(rows) {
    let labels = []
    let count = []
    let result = []
    for (let i = 0; i < rows.length; i++) {
        labels.push(rows[i].enemy)
        count.push(rows[i].count)
        result.push(rows[i].battle_result.readUInt8())
    }
    return [labels, count, result]
}


////////////// ATTACK USES API //////////////////////
app.get("/api/attack_uses", async (req, res) => {
    console.log("Attack_uses_Api Call")
    try {
        //Create a connection to the MySQL database
        const connection = await connectDB();
        // Execute a SELECT query to retrieve all users
        const [rows] = await connection.query("SELECT * FROM attack_uses");
        [labels, values] = arrange_data_for_attack_uses(rows)
        return res.json({ "labels": labels, "values": values });
    }
    catch (error) {
        console.error(error);
        res.status(500).json({ error: "Internal server error" });
    }
});

function arrange_data_for_attack_uses(rows) {
    let labels = []
    let values = []
    for (let i = 0; i < rows.length; i++) {
        labels.push(rows[i].attack)
        values.push(rows[i].times)
    }
    return [labels, values]
}

app.listen(port, () => {
    console.log(`Server running at http://localhost:${port}`);
});