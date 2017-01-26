/*
	Created 19 January 2017 by Matthew Dean
*/
drop table dbo.WordSentiment

create table dbo.WordSentiment
(
	WordId int identity(1,1),
	Word varchar(50),
	Positive int,
	Negative int,
	Anger int,
	Anticipation int,
	Disgust int,
	Fear int,
	Joy int,
	Sadness int,
	Surprise int,
	Trust int,
	primary key(WordId)
);