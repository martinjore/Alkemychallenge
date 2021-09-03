create table characters( 
 Id int not null primary key identity,
 Imagen  varchar (max)not null ,    
 Nombre  varchar(100)not null,
 Edad  int not null,
 Peso  decimal not null ,    
 Historia varchar (250) not null
 )
 create table Genres(
Id int not null primary key identity,
Imagen varchar (max) not null,
Descripcion varchar (50)not null
)
 

 create table Movies(
Id int not null primary key identity,
Imagen varchar (max) not null,
Titulo varchar(100)not null,
Fecha_Creacion datetime,
Caliﬁcacion nvarchar(1)not null,
PersAsocID int not null,
GeneroAsocID int not null,
constraint fk_Gen_Peli foreign key (GeneroAsocID) references Genres(Id),
constraint fk_Peli_Ch foreign key (PersAsocID)references characters (Id) )
