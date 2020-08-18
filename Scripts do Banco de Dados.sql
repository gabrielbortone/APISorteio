CREATE TABLE [dbo].[Administrador](
	[ID] int not null Primary key Identity,
	[UserName] NVARCHAR(256) NOT NULL,
	[Email] NVARCHAR(256) NULL,
	[NormalizedEmail] NVARCHAR(256) NULL,
	[EmailConfirmed] BIT NOT NULL,
	[PasswordHash] NVARCHAR(MAX) NULL,
	[PhoneNumber] NVARCHAR(50) NULL,
	[PhoneNumberConfirmed] BIT NOT NULL,
	[TwoFactorEnabled] BIT NOT NULL
)

GO

CREATE INDEX [IX_Administrador_NormalizedUserName] ON [dbo].[Administrador]([NormalizedEmail])

GO

CREATE INDEX [IX_Administrador_NormalizedEmail] ON [dbo].[Administrador]([NormalizedEmail])


CREATE TABLE [dbo].[ApplicationRole](
	[ID] int not null Primary key Identity,
	[Name] NVARCHAR(256) NOT NULL,
	[NormalizedName] NVARCHAR(256) NOT NULL,
)

GO

CREATE INDEX [IX_ApplicationRole_NormalizedUserName] ON [dbo].[ApplicationRole]([NormalizedName])


CREATE TABLE [dbo].[ApplicationUserRole](
	[UserId] INT NOT NULL,
	[RoleId] INT NOT NULL,
	PRIMARY KEY([UserId], [RoleId]),
	CONSTRAINT [FK_APPLICATIONUSERROLE_USER] FOREIGN KEY ([UserId]) REFERENCES [Administrador]([ID]),
	CONSTRAINT [FK_APPLICATIONUSERROLE_ROLE] FOREIGN KEY ([RoleId]) REFERENCES [ApplicationRole]([ID])
)

GO

CREATE TABLE [dbo].[Endereco](
	[EnderecoId] int not null Primary key Identity,
	[Logradouro] NVARCHAR(30) NOT NULL,
	[Bairro] NVARCHAR(30) NOT NULL,
	[Cidade] NVARCHAR(30) NOT NULL,
	[Estado] NVARCHAR(30) NOT NULL,
	[Pais] NVARCHAR(30) NOT NULL
)

CREATE TABLE [dbo].[Sorteio](
	[SorteioId] int not null Primary key Identity,
	[Titulo] NVARCHAR(40) NOT NULL,
	[Descricao] NVARCHAR(500) NOT NULL,
	[Premio] NVARCHAR(45) NOT NULL,
	[NumGanhadores] int not null,
	[Id_Administrador] int not null,
	[DataFinalizacaoCadastro] DateTime NOT NULL,
	[DataSorteio] DateTime NOT NULL,
	CONSTRAINT [FK_Sorteio_Administrador] FOREIGN KEY ([Id_Administrador]) REFERENCES [Administrador]([ID])
)

CREATE TABLE [dbo].[Participante](
	[ParticipanteId] int not null Primary key Identity,
	[Nome] NVARCHAR(30) NOT NULL,
	[Sobrenome] NVARCHAR(30) NOT NULL,
	[CPF] NVARCHAR(11) NOT NULL,
	[Email] NVARCHAR(60) NOT NULL,
	[Telefone] NVARCHAR(12) NOT NULL,
	[Id_Endereco] int not null,
	CONSTRAINT [FK_Participante_Endereco] FOREIGN KEY ([Id_Endereco]) REFERENCES [Endereco]([EnderecoId])
)

use APISorteio

CREATE TABLE [dbo].[ParticipanteSorteio](
	[ParticipanteId] int not null,
	[SorteioId] int not null,
	PRIMARY KEY([ParticipanteId], [SorteioId]),
	CONSTRAINT [FK_ParticipanteSorteio_Participante] FOREIGN KEY ([ParticipanteId]) REFERENCES [Participante]([ParticipanteId]),
	CONSTRAINT [FK_ParticipanteSorteio_Sorteio] FOREIGN KEY ([SorteioId]) REFERENCES [Sorteio]([SorteioId])
)