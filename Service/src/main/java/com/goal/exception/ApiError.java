package com.goal.exception;


import java.time.LocalDateTime;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class ApiError<T> {
	private String id;
	private LocalDateTime errorTimeDate;
	private T errors;
}
