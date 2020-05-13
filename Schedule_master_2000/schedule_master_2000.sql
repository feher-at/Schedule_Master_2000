DROP TABLE IF EXISTS tasks;
DROP TABLE IF EXISTS slots;
DROP TABLE IF EXISTS schedule_columns;
DROP TABLE IF EXISTS schedules;
DROP TABLE IF EXISTS users;
DROP FUNCTION delete_schedule(integer,integer);

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
    userid INT REFERENCES users(userid) ON DELETE CASCADE,
    title VARCHAR(50) NOT NULL
);

CREATE TABLE slots(
    slotid SERIAL PRIMARY KEY,
    schedule_columnsID INT REFERENCES schedule_columns(schedule_columnsID) ON DELETE CASCADE,
    userid INT REFERENCES users(userid) ON DELETE CASCADE

);

CREATE TABLE tasks(

    taskid SERIAL PRIMARY KEY,
    userid INT REFERENCES users(userid) ON DELETE CASCADE,
    scheduleid INT REFERENCES schedules(scheduleid) ON DELETE CASCADE,
    schedule_columnsID INT REFERENCES schedule_columns(schedule_columnsID) ON DELETE CASCADE,
    slotid INT REFERENCES slots(slotid) ON DELETE CASCADE,
    taskDate TIMESTAMP,
    taskHour int,
    title VARCHAR(50) NOT NULL,
    content VARCHAR(255) NOT NULL,
    img VARCHAR(50) NOT NULL
);

CREATE OR REPLACE FUNCTION delete_schedule(p_userid INTEGER, p_scheduleid INTEGER) RETURNS VOID AS $$
DECLARE
    v_userid INTEGER;
BEGIN
    SELECT q.userid FROM schedules AS q WHERE q.scheduleid = p_scheduleid INTO v_userid;
    IF v_userid <> p_userid THEN
        RAISE EXCEPTION 'Not authorized' USING ERRCODE = 45000;
    END IF;
    DELETE FROM schedule WHERE scheduleid = p_scheduleid;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION delete_task(p_userid INTEGER, p_taskid INTEGER) RETURNS VOID AS $$
DECLARE
    v_userid INTEGER;
BEGIN
    SELECT c.userid FROM tasks AS c WHERE c.taskid = p_taskid INTO v_userid;
    IF v_userid <> p_userid THEN
        RAISE EXCEPTION 'Not authorized' USING ERRCODE = 45000;
    END IF;
    DELETE FROM task WHERE taskid = p_taskid;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION update_schedule(p_userid INTEGER, p_scheduleid INTEGER, p_title TEXT) RETURNS VOID AS $$
DECLARE
    v_userid INTEGER;
BEGIN
    SELECT q.userid FROM schedules AS q WHERE q.scheduleid = p_scheduleid INTO v_userid;
    IF v_userid <> p_userid THEN
        RAISE EXCEPTION 'Not authorized' USING ERRCODE = 45000;
    END IF;
    UPDATE
        schedules
    SET
        title = p_title
    WHERE
        scheduleid = p_scheduleid AND
        userid = p_userid;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION update_column(p_userid INTEGER, p_columnid INTEGER, p_title TEXT) RETURNS VOID AS $$
DECLARE
    v_userid INTEGER;
BEGIN
    SELECT q.userid FROM schedule_columns AS q WHERE q.schedule_columnsid = p_columnid INTO v_userid;
    IF v_userid <> p_userid THEN
        RAISE EXCEPTION 'Not authorized' USING ERRCODE = 45000;
    END IF;
    UPDATE
        schedule_columns
    SET
        title = p_title
    WHERE
        schedule_columnsid = p_columnid AND
        userid = p_userid;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION update_task(p_userid INTEGER, p_taskid INTEGER, p_title TEXT,p_date TIMESTAMP, p_hour INTEGER, p_content TEXT, p_imgpath TEXT) RETURNS VOID AS $$
DECLARE
    v_userid INTEGER;
BEGIN
    SELECT q.userid FROM tasks AS q WHERE q.taskid = p_taskid INTO v_userid;
    IF v_userid <> p_userid THEN
        RAISE EXCEPTION 'Not authorized' USING ERRCODE = 45000;
    END IF;
    UPDATE
        tasks
    SET
        title = p_title,
        taskdate = p_date,
        taskhour = p_hour,
        content = p_content,
        img = p_imgpath
    WHERE
        taskid = p_taskid AND
        userid = p_userid;
END;
$$ LANGUAGE plpgsql;



INSERT INTO users(username, user_password, email, user_role) VALUES ('admin', 'admin', 'admin@master.com', 'admin');
INSERT INTO users(username, user_password, email, user_role) VALUES ('test', 'test', 'test@testmail.com', 'user');
INSERT INTO users(username, user_password, email, user_role) VALUES ('adamsilent', 'asdasd', 'adam.csondes@gmail.com', 'user');