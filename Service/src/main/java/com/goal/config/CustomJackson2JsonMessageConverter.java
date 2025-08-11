package com.goal.config;

import org.springframework.amqp.core.Message;
import org.springframework.amqp.support.converter.Jackson2JsonMessageConverter;
import org.springframework.amqp.support.converter.MessageConversionException;

public class CustomJackson2JsonMessageConverter extends Jackson2JsonMessageConverter {

    @Override
    public Object fromMessage(Message message) throws MessageConversionException {
        String contentType = message.getMessageProperties().getContentType();
        if (!"application/json".equals(contentType) && !"application/vnd.masstransit+json".equals(contentType)) {
            throw new MessageConversionException("Unsupported contentType: " + contentType);
        }
        return super.fromMessage(message);
    }
}
