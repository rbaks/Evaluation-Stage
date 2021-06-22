drop table Portion;
 drop table State;
 drop table Route;
 drop table City;
 go

create table City (
    id int IDENTITY(1,1) PRIMARY KEY,
    name varchar(30) NOT NULL
);

create table Route (
    id int IDENTITY(1,1) PRIMARY KEY,
    name varchar(20) not null,
    start_city int NOT NULL,
    end_city int NOT NULL,
    kmlength DECIMAL(5,2) NOT NULL check (kmlength > 0),
    FOREIGN KEY (start_city) references City (id),
    FOREIGN KEY (end_city) references City (id)
);

create table State (
    id int IDENTITY(1,1) PRIMARY KEY,
    label varchar(20) NOT NULL,
    coeffDeg DECIMAL(10, 2) NOT NULL check (coeffDeg >= 0 and coeffDeg <= 1),
    perKmCost DECIMAL(10,2) NOT NULL check (perKmCost >= 0),
    perKmDuration DECIMAL(10,2) NOT NULL check (perKmDuration >= 0)
);

create table Portion (
    id int IDENTITY(1,1) PRIMARY KEY,
    start_portion DECIMAL(5,2) NOT NULL CHECK (start_portion >= 0),
    end_portion DECIMAL(5,2) NOT NULL CHECK (end_portion >= 0),
    route_id int NOT NULL,
    state_id int NOT NULL,
    FOREIGN KEY (route_id) references Route (id),
    FOREIGN KEY (state_id) references State (id)
);

create table Budget (
    id int IDENTITY(1, 1) PRIMARY KEY,
    entrees DECIMAL(19, 5) NOT NULL CHECK (entrees > 0),
    date_entree Datetime
);

create table Reparation (
    id int IDENTITY(1, 1) PRIMARY KEY,
    route_id int not null,
    date_rep Datetime,
    prix_reparation DECIMAL(19, 5) NOT NULL CHECK (prix_reparation > 0),
    durree_reparation DECIMAL(19, 5) NOT NULL CHECK (durree_reparation > 0),
    FOREIGN KEY (route_id) references Route (id)
);

ALTER TABLE City 
ADD UNIQUE (name);
go

ALTER TABLE Route 
ADD UNIQUE (name);
go

ALTER TABLE State 
ADD UNIQUE (label);
go

ALTER TABLE Portion 
ADD UNIQUE (start_portion, end_portion, route_id);
go

ALTER TABLE State
ADD CHECK (coeffDeg >= 0 and coeffDeg <= 100);
go

ALTER TABLE State DROP Column coeffDeg;
go

CREATE OR ALTER VIEW V_Route AS SELECT Route.id as route_id, Route.name, cd.name depart, ca.name arrive, kmlength FROM Route JOIN City cd ON Route.start_city = cd.id JOIN City ca ON Route.end_city = ca.id
go

UPDATE State SET coeffDeg = 80 WHERE id = 4;
go

CREATE OR ALTER VIEW v_data AS SELECT r.*, coalesce(p.id, 0) portion_id, coalesce(p.state_id, 0) state_id, coalesce(s.coeffDeg, 0) coeff, (coalesce(p.end_portion, 0) - coalesce(p.start_portion, 0)) lg FROM V_Route r LEFT JOIN Portion p ON r.route_id = p.route_id LEFT JOIN state s ON p.state_id = s.id
go

CREATE OR ALTER VIEW v_data_sum AS SELECT route_id, name, depart, arrive, SUM(lg) smlg, kmlength, (SUM(coeff * lg)) etat_gloabal FROM v_data GROUP BY route_id, name, depart, arrive, kmlength;
go

CREATE OR ALTER VIEW Etat AS SELECT CONCAT(route_id, " ") routeid, name, depart, arrive, CASE WHEN smlg = 0 THEN "--" ELSE CONCAT(100 - (etat_gloabal / smlg), " ") END AS etatglobal FROM v_data_sum


