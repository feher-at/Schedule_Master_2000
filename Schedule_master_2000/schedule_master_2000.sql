DROP TABLE IF EXISTS slots_with_tasks;
DROP TABLE IF EXISTS tasks;
DROP TABLE IF EXISTS slots;
DROP TABLE IF EXISTS schedule_columns;
DROP TABLE IF EXISTS schedules;
DROP TABLE IF EXISTS users;

DROP FUNCTION delete_schedule(integer,integer);

CREATE TABLE users(
    userid SERIAL PRIMARY KEY,
    username VARCHAR(50) NOT NULL,
    user_password VARCHAR(255) NOT NULL,
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
    userid INT REFERENCES users(userid) ON DELETE CASCADE,
    slot_hour INT
);

CREATE TABLE tasks(

    taskid SERIAL PRIMARY KEY,
    userid INT REFERENCES users(userid) ON DELETE CASCADE,
    slotid INT REFERENCES slots(slotid) ON DELETE CASCADE,
    title VARCHAR(50) NOT NULL,
    content VARCHAR(255) NOT NULL
   
);

CREATE TABLE slots_with_tasks(
    id SERIAL PRIMARY KEY,
    slotid INT REFERENCES slots(slotid) ON DELETE CASCADE,
    userid INT REFERENCES users(userid) ON DELETE CASCADE,
    taskid INT REFERENCES tasks(taskid) ON DELETE CASCADE
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

CREATE OR REPLACE FUNCTION update_task(p_userid INTEGER, p_taskid INTEGER, p_title TEXT, p_content TEXT ) RETURNS VOID AS $$
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
        content = p_content
        
    WHERE
        taskid = p_taskid AND
        userid = p_userid;
END;
$$ LANGUAGE plpgsql;



INSERT INTO users(username, user_password, email, user_role) VALUES ('admin', '5fd924625f6ab16a19cc9807c7c506ae1813490e4ba675f843d5a10e0baacdb8', 'admin@master.com', 'admin');
INSERT INTO users(username, user_password, email, user_role) VALUES ('test', '5fd924625f6ab16a19cc9807c7c506ae1813490e4ba675f843d5a10e0baacdb8', 'test@testmail.com', 'user');
INSERT INTO users(username, user_password, email, user_role) VALUES ('adamsilent', '5fd924625f6ab16a19cc9807c7c506ae1813490e4ba675f843d5a10e0baacdb8', 'adam.csondes@gmail.com', 'user');
INSERT INTO schedules(userid,title) VALUES(2,'test schedule');
INSERT INTO schedule_columns(scheduleid,userid,title) VALUES (1,2,'Monday');
INSERT INTO schedule_columns(scheduleid,userid,title) VALUES (1,2,'Tuesday ');
INSERT INTO schedule_columns(scheduleid,userid,title) VALUES (1,2,'Wednesday');
INSERT INTO schedule_columns(scheduleid,userid,title) VALUES (1,2,'Thursday ');
INSERT INTO schedule_columns(scheduleid,userid,title) VALUES (1,2,'Friday');
INSERT INTO schedule_columns(scheduleid,userid,title) VALUES (1,2,'Saturday');
INSERT INTO schedule_columns(scheduleid,userid,title) VALUES (1,2,'Sunday');
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(1,2,1);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(1,2,2);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(1,2,3);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(1,2,4);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(1,2,5);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(1,2,6);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(1,2,7);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(1,2,8);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(1,2,9);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(1,2,10);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(1,2,11);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(1,2,12);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(1,2,13);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(1,2,14);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(1,2,15);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(1,2,16);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(1,2,17);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(1,2,18);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(1,2,19);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(1,2,20);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(1,2,21);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(1,2,22);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(1,2,23);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(1,2,24);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(2,2,1);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(2,2,2);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(2,2,3);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(2,2,4);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(2,2,5);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(2,2,6);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(2,2,7);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(2,2,8);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(2,2,9);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(2,2,10);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(2,2,11);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(2,2,12);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(2,2,13);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(2,2,14);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(2,2,15);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(2,2,16);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(2,2,17);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(2,2,18);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(2,2,19);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(2,2,20);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(2,2,21);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(2,2,22);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(2,2,23);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(2,2,24);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(3,2,1);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(3,2,2);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(3,2,3);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(3,2,4);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(3,2,5);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(3,2,6);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(3,2,7);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(3,2,8);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(3,2,9);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(3,2,10);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(3,2,11);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(3,2,12);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(3,2,13);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(3,2,14);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(3,2,15);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(3,2,16);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(3,2,17);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(3,2,18);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(3,2,19);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(3,2,20);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(3,2,21);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(3,2,22);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(3,2,23);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(3,2,24);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(4,2,1);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(4,2,2);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(4,2,3);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(4,2,4);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(4,2,5);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(4,2,6);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(4,2,7);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(4,2,8);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(4,2,9);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(4,2,10);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(4,2,11);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(4,2,12);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(4,2,13);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(4,2,14);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(4,2,15);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(4,2,16);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(4,2,17);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(4,2,18);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(4,2,19);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(4,2,20);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(4,2,21);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(4,2,22);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(4,2,23);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(4,2,24);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(5,2,1);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(5,2,2);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(5,2,3);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(5,2,4);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(5,2,5);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(5,2,6);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(5,2,7);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(5,2,8);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(5,2,9);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(5,2,10);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(5,2,11);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(5,2,12);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(5,2,13);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(5,2,14);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(5,2,15);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(5,2,16);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(5,2,17);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(5,2,18);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(5,2,19);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(5,2,20);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(5,2,21);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(5,2,22);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(5,2,23);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(5,2,24);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(6,2,1);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(6,2,2);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(6,2,3);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(6,2,4);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(6,2,5);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(6,2,6);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(6,2,7);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(6,2,8);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(6,2,9);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(6,2,10);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(6,2,11);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(6,2,12);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(6,2,13);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(6,2,14);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(6,2,15);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(6,2,16);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(6,2,17);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(6,2,18);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(6,2,19);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(6,2,20);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(6,2,21);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(6,2,22);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(6,2,23);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(6,2,24);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(7,2,1);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(7,2,2);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(7,2,3);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(7,2,4);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(7,2,5);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(7,2,6);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(7,2,7);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(7,2,8);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(7,2,9);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(7,2,10);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(7,2,11);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(7,2,12);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(7,2,13);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(7,2,14);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(7,2,15);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(7,2,16);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(7,2,17);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(7,2,18);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(7,2,19);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(7,2,20);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(7,2,21);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(7,2,22);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(7,2,23);
INSERT INTO slots(schedule_columnsid,userid,slot_hour) VALUES(7,2,24);
INSERT INTO tasks(userid,slotid,title,content)VALUES(2,7,'breakfast','i ate some breakfast');
INSERT INTO tasks(userid,slotid,title,content)VALUES(2,27,'sleep','i am sleeping');
INSERT INTO tasks(userid,slotid,title,content)VALUES(2,56,'shower','i take a shower');
INSERT INTO tasks(userid,slotid,title,content)VALUES(2,82,'shop','i need to go to the shop');
INSERT INTO tasks(userid,slotid,title,content)VALUES(2,110,'exercise','i go to the gym');
INSERT INTO tasks(userid,slotid,title,content)VALUES(2,132,'launch','i have launch');
INSERT INTO tasks(userid,slotid,title,content)VALUES(2,164,'cinema','i go to the cinema');
INSERT INTO slots_with_tasks(slotid,userid,taskid)VALUES(3,2,2);
INSERT INTO slots_with_tasks(slotid,userid,taskid)VALUES(31,2,1);
INSERT INTO slots_with_tasks(slotid,userid,taskid)VALUES(56,2,3);
INSERT INTO slots_with_tasks(slotid,userid,taskid)VALUES(81,2,4);
INSERT INTO slots_with_tasks(slotid,userid,taskid)VALUES(111,2,5);
INSERT INTO slots_with_tasks(slotid,userid,taskid)VALUES(132,2,6);
INSERT INTO slots_with_tasks(slotid,userid,taskid)VALUES(164,2,7);