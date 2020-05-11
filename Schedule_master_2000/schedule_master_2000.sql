DROP TABLE IF EXISTS tasks;
DROP TABLE IF EXISTS slots;
DROP TABLE IF EXISTS schedule_columns;
DROP TABLE IF EXISTS schedules;
DROP TABLE IF EXISTS users;

CREATE TABLE users(
    userid SERIAL PRIMARY KEY,
    username VARCHAR(50) NOT NULL,
    user_password VARCHAR(50) NOT NULL,
    email VARCHAR(50) NOT NULL,
    user_role VARCHAR(6) NOT NULL
);

CREATE TABLE schedules(
    scheduleid SERIAL PRIMARY KEY,
    userid INT REFERENCES users(userid) ON DELETE CASCADE,
    title VARCHAR(50) NOT NULL
);

CREATE TABLE schedule_columns(
    schedule_columnsID SERIAL PRIMARY KEY,
    scheduleid INT REFERENCES schedules(scheduleid) ON DELETE CASCADE,
    title VARCHAR(50) NOT NULL
);

CREATE TABLE slots(
    slotid SERIAL PRIMARY KEY,
    schedule_columnsID INT REFERENCES schedule_columns(schedule_columnsID) ON DELETE CASCADE
);

CREATE TABLE tasks(
    taskid SERIAL PRIMARY KEY,
    slotid INT REFERENCES slots(slotid) ON DELETE CASCADE,
    title VARCHAR(50) NOT NULL,
    content VARCHAR(255) NOT NULL,
    img VARCHAR(50) NOT NULL
);