package com.goal.Controller;

import java.util.List;
import java.util.UUID;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.goal.DTO.CreateGoalDto;
import com.goal.DTO.GetGoalDto;
import com.goal.DTO.UpdateGoalDto;

import jakarta.validation.Valid;

@RestController
@RequestMapping("/api/goals")
public class GoalController {
    
    @Autowired private com.goal.IService.IGoalService _goalService;

    @PostMapping("/create")
	public ResponseEntity<Void> createGoal(@RequestBody @Valid CreateGoalDto dto) {
		_goalService.createGoal(dto);
		return ResponseEntity.ok().build();
	}

    @GetMapping("/{userId}")
	public ResponseEntity<GetGoalDto> getGoal(@PathVariable String userId) {
		return ResponseEntity.ok(_goalService.getGoalByUserId(userId));
	}
	@GetMapping("/allGoals/{userId}")
	public ResponseEntity<List<GetGoalDto>> getAllHomes(@PathVariable String userId) {
		return ResponseEntity.ok(_goalService.getAllGoals(userId));
	}

	@DeleteMapping("/delete/{id}")
	public ResponseEntity<Void> deleteGoal(@PathVariable UUID id) {
		_goalService.deleteGoal(id);
		return ResponseEntity.ok().build();
	}
	
	@PutMapping("/update/{id}")
	public ResponseEntity<Void> updateGoal(@PathVariable UUID id, @RequestBody @Valid UpdateGoalDto dto) {
		_goalService.updateGoal(id, dto);
		return ResponseEntity.ok().build();
	}
}
