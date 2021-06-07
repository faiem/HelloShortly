set session my.number_of_ranges = '1000';

INSERT INTO key_ranges
select key_ranges_id
	,((key_ranges_id*1000000)+1)
	,((key_ranges_id*1000000)+1000000),
    False,
	NULL,
	NULL
FROM GENERATE_SERIES(1, current_setting('my.number_of_ranges')::bigint) as key_ranges_id;