package com.goal.IRepository;

import java.util.List;
import java.util.Optional;
import java.util.UUID;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import com.goal.Entities.Goal;

@Repository
public interface  IGoalRepository extends  JpaRepository<Goal, UUID>{

    @Query("SELECT g FROM Goal g WHERE g.userId = :userId")
    Optional<Goal> findByUserId(@Param("userId") String userId);

    @Query("SELECT g FROM Goal g WHERE g.userId = :userId")
    List<Goal> findAllByUserId(@Param("userId") String userId);
}
