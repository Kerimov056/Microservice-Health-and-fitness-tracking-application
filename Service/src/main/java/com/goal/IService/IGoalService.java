package com.goal.IService;

import java.util.List;
import java.util.UUID;

import com.goal.DTO.CreateGoalDto;
import com.goal.DTO.GetGoalDto;
import com.goal.DTO.UpdateGoalDto;

public interface  IGoalService {
    void createGoal(CreateGoalDto dto);
    void updateGoal(UUID id, UpdateGoalDto dto);
    void deleteGoal(UUID id);
    GetGoalDto getGoalByUserId(String userId);
    List<GetGoalDto> getAllGoals(String userId);
}
