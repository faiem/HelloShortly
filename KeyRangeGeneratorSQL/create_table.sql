CREATE TABLE IF NOT EXISTS key_ranges (
  key_ranges_id bigint NOT NULL,
  start_range bigint NOT NULL,
  end_range bigint NOT NULL,
  is_retrieved boolean NOT NULL,
  retrieve_key varchar(40),
  retrieved_time TIMESTAMP,
  PRIMARY KEY (key_ranges_id)
);