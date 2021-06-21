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