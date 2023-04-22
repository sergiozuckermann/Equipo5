const express = require("express");
const mysql = require("mysql2/promise");


const app = express();
const port = 3000;

// Middleware to parse JSON request bodies
app.use(express.json());

// Route for creating a new user
async function connectDB(){
    const connection = await mysql.createConnection({
        host: "localhost",
        user: "root",
        password: "Tequila123",
        database: "zazzacrifice",
      });

    return connection;  
}

async function  check_if_user_exists(username)
{
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
        if (rows.length == 0)
        {
            res.status(401).json({ error: "Invalid username or password" });
            return;
        }
        res.json(rows[0].user_id);
    } catch (error) {
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
        if (await check_if_user_exists(req.body.username))
        {
            res.status(401).json({ error: "User already exists" });
            return;
        }
        // Execute a SELECT query to retrieve all users
        const [rows] = await connection.query("INSERT INTO users (username, password) VALUES (?, ?)", [req.body.username, req.body.password]);
        res.json(rows);
    } catch (error) {
        res.status(500).json({ error: "Internal server error" });
    }

});


app.get("/api/game_sessions", async (req, res) => {
    try {
        //Create a connection to the MySQL database
        const connection = await connectDB();
        // Execute a SELECT query to retrieve all users
        const [rows] = await connection.query("SELECT * FROM sessions_summary WHERE user_id = ?", [req.query.user_id]);
        res.json(rows);
    } catch (error) {
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
        res.status(500).json({ error: "Internal server error" });
    }
});


// Start the server
app.listen(port, () => {
  console.log(`Server running at http://localhost:${port}`);
});