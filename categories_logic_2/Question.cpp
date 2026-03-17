#include "Question.h"



// Конструктор по умолчанию
Question::Question()
    : content_("")
    , options_()
    , correct_options_()
    , is_resolved_(false)
    , is_correct_(false){
}

/**
 * Конструктор с текстовым вопросом
 * Создает простой вопрос с одним правильным ответом
 * @param content текст вопроса
 */
Question::Question(const std::string& content)
    : content_(content)
    , options_()  // пока нет вариантов
    , points_options_() // пока нет очков за ответы
    , is_resolved_(false) {
}

/**
 * Конструктор с вариантами ответов
 * Создает вопрос с вариантами ответов (по умолчанию MULTIPLE_CHOICE)
 * @param options вектор вариантов ответов
 */


Question::Question(const std::string& content,
    const std::vector<std::string>& options,
    const std::vector<int>& points_options)
    : content_(content)
    , options_(options)
    , points_options_(points_options)
    , is_resolved_(false)
{
}


// Геттеры и сеттеры
std::string Question::getContent() const {
    return content_;
}

std::string Question::getContentAsString() const {
    if (content_.empty()) {
        return "";
    }
        return content_;
}

void Question::setContent(const std::string &content) {
    content_ = content;
}

bool Question::isResolved() const {
    return is_resolved_;
}

void Question::setResolved(bool resolved) {
    is_resolved_ = resolved;
}

int Question::getPoints() const {
    return points_;
}

void Question::setPoints(int points) {
    points_ += points;
}

/**
 * Подсчитывает количество очков за вопрос
 */
int Question::countPoints() {
    if (!is_resolved_) {
        std::cout << "Вопрос еще не решен!" << std::endl;
        return 0;
    }
        return points_;
}

/**
 * Редактирует вопрос
 */
bool Question::edit(const std::string& newContent,
    const std::vector<std::string>& newOptions,
    const std::vector<int>& newPointsOptions,
    int newPoints) {
    bool changed = false;

    // Изменение текста вопроса
    if (!newContent.empty() && newContent != content_) {
        content_ = newContent;
        changed = true;
    }

    // Изменение вариантов ответов
    if (!newOptions.empty() && newOptions != options_) {
        options_ = newOptions;
        changed = true;
    }

    // Изменение правильных ответов
    if (!newPointsOptions.empty() && newPointsOptions != points_options_) {
        points_options_ = newPointsOptions;
        changed = true;
    }

    // Изменение объяснения
    if (!newExplanation.empty() && newExplanation != explanation_) {
        explanation_ = newExplanation;
        changed = true;
    }

    return changed;
}


/**
 * Добавляет вариант ответа
 */
void Question::addOption(const std::string& option) {
    options_.push_back(option);
    std::cout << "Вариант ответа добавлен. Всего вариантов: " << content_.size() - 1 << std::endl;
}

/**
 * Удаляет вариант ответа
 */
void Question::removeOption(int index) {
    if (content_.size() <= 1) {
        std::cout << "Ошибка: Вопрос должен содержать хотя бы текст вопроса!" << std::endl;
        return;
    }

    // Индекс в векторе смещен на 1, так как content_[0] - это текст вопроса
    int vectorIndex = index + 1;

    if (vectorIndex > 0 && vectorIndex < static_cast<int>(options_.size())) {
        options_.erase(options_.begin() + vectorIndex);
        std::cout << "Вариант ответа " << index << " удален." << std::endl;
    }
    else {
        std::cout << "Ошибка: Неверный индекс варианта ответа!" << std::endl;
    }
}

json Question::toJson() const {
    json j;
    j["content"] = content_;
    j["options"] = options_;
    j["points_options"] = points_options_;
    j["points"] = points_;
    return j;
}

void Question::fromJson(const json& j) {
    content_ = j.at("content").get<std::string>();

    if (j.contains("options")) {
        options_ = j["options"].get<std::vector<std::string>>();
    }

    if (j.contains("points_options")) {
        points_options_ = j["points_options"].get<std::vector<int>>();
    }

    points_ = j.value("points", 0);
}